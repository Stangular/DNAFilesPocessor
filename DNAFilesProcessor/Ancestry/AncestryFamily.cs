using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DNAFilesProcessor.Ancestry
{
    public class Event
    {
        public int Year { get; private set; }

        public string Date { get; private set; }
        public string Place { get; private set; }

        public Event(string date, string place)
        {
            Date = date;
            Place = place;
            int y;
            if( int.TryParse(getYear(date), out y))
            {
                Year = y;
            }
        }

        private string getYear(string dateString)
        {
            Regex regex = new Regex(@"\d{4}");
            return regex.Match(dateString).ToString();
        }

        public XElement GetAsXML()
        {
            XElement project = new XElement("Birth",
                new XElement("DOB", Date));
            project.Add(new XElement("POB", Place));
            project.Add(new XElement("Year", Year));
            return project;
        }
    }
    public class AncestryFamilyMember
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Event Birth { get; private set; }
        //public Event Death { get; private set; }
        public List<string> Admins { get; private set; }
        public List<string> ProjectMember { get; private set; }
        public bool Common { get; private set; }
        public AncestryFamilyMember(int id, string name, string dob, string pob, bool common = false)
        {
            ID = id;
            Name = name;
            Birth = new Event(dob, pob);
            Common = common;
            Admins = new List<string>();
            ProjectMember = new List<string>();
        }

        public XElement GetAsXML()
        {
            XElement project = new XElement("Member",
                new XElement("Name", Name));
            project.Add(Birth.GetAsXML());
            project.Add(new XElement("Common", Common ? 1 : 0 ));
            var aelm = new XElement("Members");
            foreach ( var a in Admins)
            {
                aelm.Add(new XElement("AdminID",a));
            }
            var pelm = new XElement("ProjectMembers");
            foreach (var p in ProjectMember)
            {
                aelm.Add(new XElement("PMember", p));
            }
            project.Add(pelm);

            return project;
        }

        public bool IncludesAdmin(string adminID)
        {
            return Admins.Contains(adminID);
        }

        public bool IncludesAdmin(List<string> adminIDs)
        {
            return Admins.FindAll(a => adminIDs.Contains(a)).Count > 0;
        }

        public void AddAdmin(string adminID)
        {
            if (Admins.FindIndex(a => a == adminID) < 0)
            {
                Admins.Add(adminID);
            }
        }
        
        public void AddProjectMember(string projectMemberID)
        {
            if (ProjectMember.FindIndex(a => a == projectMemberID) < 0)
            {
                ProjectMember.Add(projectMemberID);
            }
        }

        public int BirthYear
        {
            get
            {
                return Birth.Year;
            }
        }
        public string BirthDate
        {
            get
            {
                return Birth.Date;
            }
        }

        public string BirthPlace
        {
            get
            {
                return Birth.Place;
            }
        }

        public int AdminCount
        {
            get
            {
                return Admins.Count;
            }
        }

        public int ProjectMemberCount
        {
            get
            {
                return ProjectMember.Count;
            }
        }

    }

    public class AncestryFamily
    {
        public string Surname { get; private set; }
        public List<AncestryFamilyMember> Members { get; private set; }
        public AncestryFamily(string surname)
        {
            Surname = surname;
            Members = new List<AncestryFamilyMember>();
        }

        public bool IncludesAdmin(string adminID)
        {
            if( Members.FindIndex(m => m.IncludesAdmin(adminID)) >= 0 )
            {
                return true;
            }
            return  false;
        }

        public XElement GetAsXML()
        {
            XElement project = new XElement("Family",
                new XElement("Surname", Surname));
            foreach (var m in Members)
            {
                project.Add(m.GetAsXML());
            }
            return project;
        }

        public bool IncludesAdmin(List<string> adminIDs)
        {
            return Members.FindIndex(m => m.IncludesAdmin(adminIDs)) >= 0;
        }

        public int MemberCount { get { return Members.Count;  } }
        public void AddMembers(AncestryFamily family)
        {
            Members.AddRange(family.Members);
        }
        public void AddMember(string name, string dob, string pob, string adminID, string projectMemberID, bool common = false)
        {
            var member = Members.FirstOrDefault(m => m.Name == name);
            if( member == null )
            {
                member = new AncestryFamilyMember(Members.Count, name, dob, pob);
                Members.Add(member);
            }
            member.AddAdmin(adminID);
            member.AddProjectMember(projectMemberID);
        }

        public void Fix()
        {
            Surname = Surname.ToLower();
            Regex regex = new Regex(@"[^a-zA-Z]");
            Surname = regex.Replace(Surname,"");
        }
        public void Format(List<AncestryMember> commonMembers)
        {
            Members = Members.OrderByDescending(m => m.Name).ThenByDescending(m => m.Birth.Date).ToList();
            List<int> removeList = new List<int>();
            for(int i = 0; i < Members.Count; i++)
            {
                var mi = Members[i];
                for( int j = i + 1; j < Members.Count; j++)
                {
                    var mj = Members[j];
                    if(( mi.Name.Contains(mj.Name) || mj.Name.Contains(mi.Name )) 
                        && (mi.Birth.Date == mj.Birth.Date || mi.Birth.Year > mj.Birth.Year - 5 && mi.Birth.Year < mj.Birth.Year + 5 ))
                    {// Remove common individuals for different admins
                        mi.AddAdmin(mj.Admins[0]);
                        mi.AddProjectMember(mj.ProjectMember[0]);
                        if(!removeList.Contains(j))
                        {
                            removeList.Add(j);
                        }
                    }
                }
            }
            removeList = removeList.OrderByDescending( i => i ).ToList();
            foreach ( int i in removeList)
            {
                Members.RemoveAt(i);
            }

            removeList.Clear();
            for (int i = 0; i < Members.Count; i++)
            {
                if ( Members[i].Admins.Count < 2 
                    && commonMembers.FindIndex(c => Members[i].Admins.Contains(c.AdminID)) < 0)
                {// Remove if only one admin and not common between admins.
                    if (!removeList.Contains(i))
                    {
                        removeList.Add(i);
                    }
                }
            }
            removeList = removeList.OrderByDescending(i => i).ToList();
            foreach (int i in removeList)
            {
                Members.RemoveAt(i);
            }
            // Remove all members that only exist for one admin for one member...
            Members = Members.Where(m => m.Admins.Count > 1 || m.ProjectMember.Count > 1).ToList();

        }
    }

    public class AncestryFamilies
    {
        List<AncestryFamily> _families = new List<AncestryFamily>();

        public AncestryFamilies()
        {

        }

        public List<AncestryFamily> FamiliesForAdmin(string adminID = "")
        {
            if( String.IsNullOrWhiteSpace(adminID))
            {
                return _families;
            }
            return _families.Where(f => f.IncludesAdmin(adminID)).ToList();
        }

        public List<AncestryFamily> FamiliesForAdmin(List<string> adminIDs)
        {
            return _families.Where(f => f.IncludesAdmin(adminIDs)).ToList();
        }

        public List<AncestryFamily> Families
        {
            get { return _families; }
        }
        public void AddFamilyMember(string surname, string name, string dob, string pob, string admin, string memberID, bool common = false)
        {
            //var family = _families.FirstOrDefault(f => f.Surname == surname);
            //if (family == null)
            //{
            //    family = new AncestryFamily(surname);
            //    _families.Add(family);
            //}
            var family = new AncestryFamily(surname);
            family.AddMember(name, dob, pob, admin, memberID, common);
            _families.Add(family);

        }

        public bool Format(List<AncestryMember> commonMembers)
        {
            _families.ForEach(f => f.Fix());
            _families = _families.Where(f => f.Surname.Length > 2).ToList();
            var famgroup = _families.GroupBy(f => f.Surname).Select(grp=>grp.ToList()).ToList();

            var removedGroups = famgroup.Where(f => f.Count <= 2).ToList();
            var filteredGroups = famgroup.Where(f => f.Count > 2).ToList();
            _families.Clear();
            foreach( var g in filteredGroups)
            {
                var f1 = g[0];
                g.RemoveAt(0);
                foreach( var f in g)
                {
                    f1.AddMembers(f);
                }
                _families.Add(f1);
            }
            _families = _families.OrderBy(f => f.Surname).ToList();

            Parallel.ForEach(_families, f => { f.Format(commonMembers); });
            _families = _families.Where(f => f.Members.Count > 1).ToList();
            return true;
        }
    }
}
