using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using DNAFilesProcessor.Ancestry;

namespace DNAFilesProcessor
{
    public delegate void ProgessUpdateDelegate(long progress);
    public delegate void FileProcessingUpdateDelegate(string meggase);

    public class DNAFile
    {
        public DNAFile(int fileType, string filePathName)
        {
            FilePathName = filePathName;
            FileType = fileType;
        }
        public DNAFile(XElement dnafileNode)
        {
            List<XElement> type = dnafileNode.Descendants("Type").ToList();
            List<XElement> name = dnafileNode.Descendants("Name").ToList();
            int fileType;
            Int32.TryParse(type[0].Value, out fileType);
            FileType = fileType;
            FilePathName = name[0].Value;
        }
        public int FileType { get; private set; }
        public string FilePathName { get; private set; }
        public string FileName
        {
            get { return System.IO.Path.GetFileName(FilePathName); }
        }

        public XElement GetAsXML()
        {
            return new XElement("DNAFile",
                new XElement("Type", FileType.ToString()),
                new XElement("Name", FilePathName));
        }
    }

    public class ProjectMember
    {
        List<DNAFile> _files = new List<DNAFile>();
        public ProjectMember(string memberID)
        {
            MemberID = memberID;
        }
        public ProjectMember(XElement member)
        {
            MemberID = "";
            if (null != member)
            {
                XElement id = member.Descendants("ID").FirstOrDefault();
                XElement name = member.Descendants("Name").FirstOrDefault();
                XElement notes = member.Descendants("Notes").FirstOrDefault();
                if (null != id)
                {
                    MemberID = id.Value;
                    if (null != name)
                    {
                        Name = name.Value;
                    }
                    if (null != notes)
                    {
                        Notes = notes.Value;
                    }
                }
                _files.AddRange(member
                    .Descendants("DNAFile")
                    .Select(f => new DNAFile(f)).ToList());
            }

        }

        public string MemberID { get; set; }

        public void AddDNAFile(int fileType, string filePath)
        {
            _files.Add(new DNAFile(fileType, filePath));
        }

        public void Update(string name, string notes)
        {
            Name = name;
            Notes = notes;
        }

        public string Name { get; private set; }
        public string Notes { get; private set; }
        public List<DNAFile> Files
        {
            get { return _files; }
        }

        public void ClearFiles()
        {
            _files.Clear();
        }

        public XElement GetAsXML()
        {

            XElement member = new XElement("Member",
                 new XElement("ID", MemberID.ToString()),
                 new XElement("Name", Name),
                 new XElement("Notes", Notes));
            foreach (var f in _files)
            {
                member.Add(f.GetAsXML());
            }
            return member;
        }


    }

    public class SurnameProject
    {
        int _selectedIndex = -1;
        ProjectMember _emptyMember = new ProjectMember("");
        private List<ProjectMember> _members = new List<ProjectMember>();
        public SurnameProject(string surname)
        {
            Surname = surname;
        }
        public SurnameProject(XElement project)
        {
            List<XElement> items = project.Descendants("SurName").ToList();
            if (items.Count > 0)
            {
                Surname = items[0].Value;
                _members.AddRange(project
                    .Descendants("Member")
                    .Select(m => new ProjectMember(m))
                    .ToList());
            }
        }
        public List<ProjectMember> Members
        {
            get { return _members; }
        }

        public string Surname { get; set; }

        public ProjectMember Member
        {
            get
            {
                if (_selectedIndex < 0)
                {
                    return _emptyMember;
                }
                return _members[_selectedIndex];
            }
        }
        public bool SelectMember(string memberID)
        {
            _selectedIndex = _members.FindIndex(m => m.MemberID == memberID);
            return _selectedIndex >= 0;
        }
        public bool AddMember(string memberName)
        {
            if (!String.IsNullOrWhiteSpace(memberName)
            && !IsMember(memberName))
            {
                _selectedIndex = _members.Count;
                _members.Add(new ProjectMember(memberName));
                return true;
            }
            return false;
        }

        public bool IsMember(string memberID)
        {
            return _members.Exists(m => m.MemberID == memberID);
        }


        public void ClearFile(string memberID)
        {
            var member = _members.FirstOrDefault(m => m.MemberID == (memberID ?? ""));
            if (member != null)
            {
                member.ClearFiles();
            }
        }

        public void ClearAllFiles()
        {
            foreach (var m in _members)
            {
                m.ClearFiles();
            }
        }

        public XElement GetAsXML()
        {
            XElement project = new XElement("Project",
                new XElement("SurName", Surname));
            foreach (var m in _members)
            {
                project.Add(m.GetAsXML());
            }
            return project;
        }

        public void UI(System.Windows.Forms.ListControl uiControl)
        {
            uiControl.DisplayMember = "MemberID";
            uiControl.ValueMember = "MemberID";
            uiControl.DataSource = _members;
        }
    }


    public class Projects
    {

        Matches _matches = new Matches();
        Segments _segments = new Segments();
        Dictionary<string, List<string>> admins = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> families = new Dictionary<string, List<string>>();
        private List<AncestryMember> allMembers = new List<AncestryMember>();
        AncestryFamilies _ancestryFamilies = new AncestryFamilies();

        SurnameProject _emptyProject = new SurnameProject("Unknown Project");
        List<SurnameProject> _projects = new List<SurnameProject>();
        int _selectedIndex = -1;

        public ProgessUpdateDelegate mProgessDelegate;
        public FileProcessingUpdateDelegate mFileProgressDelegate;
        public Projects()
        {

        }
        public bool ValidProject
        {
            get { return _selectedIndex >= 0; }
        }
        public bool SelectProject(string surname)
        {
            _selectedIndex = _projects.FindIndex(m => m.Surname == surname);
            return _selectedIndex >= 0;
        }
        public bool AddProject(string surname)
        {
            if (!String.IsNullOrWhiteSpace(surname)
            && !SelectProject(surname))
            {
                if (!String.IsNullOrWhiteSpace(surname))
                {
                    _selectedIndex = _projects.Count;
                    _projects.Add(new SurnameProject(surname));
                }
            }
            return _selectedIndex >= 0;
        }

        public List<AncestryMember> MemberAdmins
        {
            get
            {
                return allMembers;
            }
        }
        public bool ContainsProject(string surname)
        {
            return _projects.Exists(p => p.Surname == surname);
        }
        public bool ContainsMember(string memberID)
        {
            return _projects[_selectedIndex].IsMember(memberID);
        }
        public void Reset()
        {
            _matches.Reset();
        }
        public SurnameProject Project
        {
            get
            {
                if (_selectedIndex < 0)
                {
                    if (_projects.Count <= 0)
                    {
                        return _emptyProject;
                    }
                    _selectedIndex = 0;
                }
                return _projects[_selectedIndex];
            }
        }
        
        public async void ProcessMembers(List<string> processMembers)
        {
            var members = Project.Members.FindAll(m => processMembers.Contains(m.MemberID));
            //foreach (var m in members)
            //{
            //    foreach (var f in m.Files)
            //    {
            //        List<string> data = System.IO.File.ReadAllLines(f.FilePathName).ToList();
            //        data.RemoveRange(0, 1);
            //        switch (f.FileType)
            //        {
            //            case 0: ParseFTDNAMatches(m.MemberID, data); break;
            //        }
            //    }
            //}
            //foreach (var m in members)
            //{
            //    foreach (var f in m.Files)
            //    {
            //        List<string> data = System.IO.File.ReadAllLines(f.FilePathName).ToList();
            //        data.RemoveRange(0, 1);
            //        switch (f.FileType)
            //        {
            //            case 1: ParseFTFNASegments(m.MemberID, data); break;
            //        }
            //    }
            //}

            foreach (var m in members)
            {
                var file = m.Files.FirstOrDefault(f => f.FileType == 2);
                if (file != null)
                {
                    foreach (string line in System.IO.File.ReadLines(file.FilePathName))
                    {
                        ParseAncestryMatches(m.MemberID, line.Split(new string[] { "," }, StringSplitOptions.None));
                    }
                }
            }
            allMembers = admins.Where(a => a.Value.Count > 1).Select(a => new AncestryMember(a.Key, false, a.Value)).OrderBy(a => a.ProjectMembers.Count).ToList();

            foreach (var m in members)
            {
                var task1 = Task.Run(() => ParseMemberICWFile(m));
                var result1 = await task1;
            }
            foreach (var m in members)
            {
                var task2 = Task.Run(() => ParseMemberFamilyFile(m));
                var result2 = await task2;
            }

            var task3 = Task.Run(() => FormatAllData());
            var result3 = await task3;

        }

        public AncestryFamilies CommonFamilies
        {
            get { return _ancestryFamilies; }
        }
        private bool FormatAllData()
        {
            mFileProgressDelegate.Invoke("0 Formatting all project data");
            _ancestryFamilies.Format(allMembers);
            mFileProgressDelegate.Invoke("2 Formatting all project data");
            return true;
        }
        private bool ParseMemberICWFile(ProjectMember member)
        {
            var file = member.Files.FirstOrDefault(f => f.FileType == 3);
            if (file != null)
            {
                long processedSz = 0;
                var fstrm = System.IO.File.Open(file.FilePathName, System.IO.FileMode.Open);
                long sz = fstrm.Length;
                fstrm.Close();
                mFileProgressDelegate.Invoke("0 ICW for " + member.MemberID);
                foreach (string line in System.IO.File.ReadLines(file.FilePathName))
                {
                    ParseMemberICW(member, line.Split(new string[] { "," }, StringSplitOptions.None), sz);
                    processedSz += line.Length;
                    mProgessDelegate.Invoke((long)(((float)processedSz / (float)sz) * 100.00));
                }
                mProgessDelegate.Invoke(0);
                mFileProgressDelegate.Invoke("2 ICW for " + member.MemberID);
            }
            return true;
        }
        private void ParseMemberICW(ProjectMember member, string[] line, long filesz)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"");
            string adminID = regex.Replace(line[2], "");
            var adminMember = allMembers.FirstOrDefault(m => m.AdminID == adminID);
            if (adminMember != null)
            {
                regex = new System.Text.RegularExpressions.Regex("\"");
                string ICWID = regex.Replace(line[5], "");
                AncestryMember icwmember = allMembers.FirstOrDefault(a => a.AdminID == ICWID);
                if (icwmember == null)
                {
                    icwmember = new AncestryMember(ICWID, true);
                    allMembers.Add(icwmember);
                }
                icwmember.addICW(adminID);
                adminMember.addICW(ICWID);
            }
        }

        private bool ParseMemberFamilyFile(ProjectMember member)
        {
            var file = member.Files.FirstOrDefault(f => f.FileType == 4);
            if (file != null)
            {
                long processedSz = 0;
                var fstrm = System.IO.File.Open(file.FilePathName, System.IO.FileMode.Open);
                long sz = fstrm.Length;
                fstrm.Close();
                mFileProgressDelegate.Invoke("0 families for " + member.MemberID);

                foreach (string line in System.IO.File.ReadLines(file.FilePathName))
                {
                    try
                    {
                        var lineplaces = line.Split(new string[] { ",\"" }, StringSplitOptions.None);
                        ParseMemberFamilies(member.MemberID, line.Split(new string[] { "," }, StringSplitOptions.None), lineplaces);
                        processedSz += line.Length;
                        mProgessDelegate.Invoke((long)(((float)processedSz / (float)sz) * 100.00));
                    }
                    catch (System.Exception ex)
                    {
                        EventLog.WriteEntry("ParseMemberFamilyFile", ex.Message);
                        mFileProgressDelegate.Invoke("3 families error for " + member.MemberID);
                    }
                }
                mProgessDelegate.Invoke(0);
                mFileProgressDelegate.Invoke("2 families for " + member.MemberID);

            }
            return true;
        }

        private void ParseMemberFamilies(string memberID, string[] line, string[] lineplaces)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"");
            string admin = regex.Replace(line[3], "");
            string surname = regex.Replace(line[4], "");
            string given = regex.Replace(line[5], "");
            string dob = regex.Replace(line[6], "");
            string pob = "";

            if (lineplaces.Length == 0)
            {
                pob = regex.Replace(line[8], "");
            }
            else if (line.Length > 8)
            {
                pob = regex.Replace(line[8], "");
            }
            else
            {
                pob = regex.Replace(lineplaces[1], "");
            }

            _ancestryFamilies.AddFamilyMember(surname, given, dob, pob, admin, memberID, false);
        }

        private void ParseAncestryMatches(string memberID, string[] line)
        {
            if (line.Length < 4)
            {
                return;
            }
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"");
            string adminID = regex.Replace(line[3], "");
            if (!admins.ContainsKey(adminID))
            {
                admins.Add(adminID, new List<string>());
            }
            if (admins[adminID].IndexOf(memberID) < 0)
            {
                admins[adminID].Add(memberID);
            }
        }

        private void ParseFTFNASegments(string memberID, List<string> data)
        {
            foreach (string s in data)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"");
                string res = regex.Replace(s, "");

                var line = res.Split(new string[] { "," }, StringSplitOptions.None);
                _segments.AddNewMemberSegment(memberID, line[0]);
                _segments.AddSegment(line);
                // s.Replace()
                //if (parts.Length == 1)
                //{
                //    string[] p = parts[0].Split(',');
                //    //  families = parts[1];
                //    _matches.AddMatch(memberID, p[0].Trim(), p[11]);
                //}
                //else if (parts.Length == 2)
                //{
                //    string[] p = parts[0].Split(',');
                //    _matches.AddMatch(memberID, p[0].Trim(), parts[1]);
                //}
                //else if (parts.Length == 16)
                //{
                //    parts[0].Split();
                //    _matches.AddMatch(memberID, parts[0].Trim('\"').Trim(), parts[11]);
                //}
                //else
                //{
                //    //  MessageBox.Show("Unexpected Data Line: " + parts.Length.ToString());
                //    // _matches.LineFail(memberID,filename,lineNumber,s);
                //    continue;
                //}
                //     string[] parts = s.Split()
                //    string [] parts = line.Split(',');
                //    _matches.AddMatch(memberID,parts[0].Trim());
            }

        }
        private void ParseFTDNAMatches(string memberID, List<string> data)
        {
            foreach (string s in data)
            {
                // string[] parts = System.Text.RegularExpressions.Regex.Split(s, ",\"/g");
                string[] parts = s.Split(new string[] { ",\"" }, StringSplitOptions.None);
                if (parts.Length == 1)
                {
                    string[] p = parts[0].Split(',');
                    //  families = parts[1];
                    _matches.AddMatch(memberID, p[0].Trim(), p[11]);
                }
                else if (parts.Length == 2)
                {
                    string[] p = parts[0].Split(',');
                    _matches.AddMatch(memberID, p[0].Trim(), parts[1]);
                }
                else if (parts.Length == 16)
                {
                    parts[0].Split();
                    _matches.AddMatch(memberID, parts[0].Trim('\"').Trim(), parts[11]);
                }
                else
                {
                    //  MessageBox.Show("Unexpected Data Line: " + parts.Length.ToString());
                    // _matches.LineFail(memberID,filename,lineNumber,s);
                    continue;
                }
                //     string[] parts = s.Split()
                //    string [] parts = line.Split(',');
                //    _matches.AddMatch(memberID,parts[0].Trim());
            }
        }

        public void Output(string[] secondaryList, string path)
        {
            _matches.Output(_segments, secondaryList.ToList(), path);
        }
        public void Complete()
        {
            _segments.Finish(_matches);
            _matches.Finish();
            //   string x = _matches.Output(_segments, memberSearchList);

            // Order matches by segment frequency/total segment group cM - get families from those...
        }

        public void Save(string filename)
        {
            if (String.IsNullOrWhiteSpace(filename))
            {
                return;
            }
            XDocument doc = new XDocument();
            XElement root = new XElement("Projects");

            foreach (var p in _projects)
            {
                root.Add(p.GetAsXML());
            }
            doc.Add(root);
            doc.Save(filename);
        }

        public void SaveAncestry(string filename)
        {
            if (String.IsNullOrWhiteSpace(filename))
            {
                return;
            }
            XDocument doc = new XDocument();
            XElement root = new XElement("Ancestry");
            foreach ( var a in allMembers)
            {
                root.Add(a.GetAsXML());
            }
            foreach (var f in _ancestryFamilies.Families)
            {
                root.Add(f.GetAsXML());
            }
            doc.Add(root);
            doc.Save(filename);
        }

        public void OpenProjects(string fileName)
        {
            _projects.AddRange(XDocument.Load(fileName)
                .Descendants("Project")
                .Select(p => new SurnameProject(p)).ToList());
        }

        public void UI(System.Windows.Forms.ListControl uiControl)
        {
            uiControl.DisplayMember = "Surname";
            uiControl.ValueMember = "Surname";
            uiControl.DataSource = _projects;
        }
    }


}
