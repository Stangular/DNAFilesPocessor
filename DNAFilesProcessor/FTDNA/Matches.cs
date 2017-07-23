using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DNAFilesProcessor
{
    public class Family
    {
        int _ID = -1;
        string _surname = "";
        List<int> _matches = new List<int>();
        List<KeyValuePair<int, int>> _orderedStack = new List<KeyValuePair<int, int>>();
        Dictionary<int, int> _stacksMostWith = new Dictionary<int, int>();
        double _totalcM = 0.0;
        public Family(int id, string surname)
        {
            _ID = id;
            _surname = surname;
        }
        public int ID
        {
            get { return _ID; }
        }
        public string Surname
        {
            get { return _surname; }
        }
        public double AverageCM
        {
            get { return _totalcM / _matches.Count; }
        }
        // public 
        public void AddMatch(int matchID, double cM)
        {
            _matches.Add(matchID);
            _totalcM += cM;
        }
        public List<KeyValuePair<int, int>> OrderedStack
        {
            get { return _orderedStack.FindAll(v => v.Value > 1); }
        }
        public void AddStack(List<int> stack)
        {
            foreach (int fid in stack)
            {
                if (fid == _ID)
                {
                    continue;
                }
                if (!_stacksMostWith.ContainsKey(fid))
                {
                    _stacksMostWith.Add(fid, 0);
                }
                _stacksMostWith[fid] += 1;
            }
        }
        public int StackCount
        {
            get { return _stacksMostWith.Count; }
        }
        public int MatchCount
        {
            get { return _matches.Count; }
        }
        public double TotalcM
        {
            get { return _totalcM; }
        }
        public bool ContainsMatch(int matchID)
        {
            return _matches.IndexOf(matchID) >= 0;
        }
        public bool ContainsMatch(List<int> matchIDList)
        {
            if (matchIDList.Count <= 0)
            {
                return true;
            }
            var matches = _matches.FindAll(m => matchIDList.Contains(m));
            return null != matches && matches.Count > 0;
        }
        public void Complete()
        {
            _orderedStack = _stacksMostWith.OrderByDescending(x => x.Value).ToList();
        }
        public int ClosestMatchCount()
        {
            KeyValuePair<int, int> pair = _orderedStack.FirstOrDefault();
            return pair.Value;
        }
    }

    public class Families
    {
        List<Family> _families = new List<Family>();

        public Families()
        {

        }
        public void Clear()
        {
            _families.Clear();
        }
        public void Sort(int type = 0)
        {
            OrderedParallelQuery<Family> q;
            switch (type)
            {
                case 1: q = _families.AsParallel().OrderBy(f => f.MatchCount); break;
                case 2: q = _families.AsParallel().OrderBy(f => f.TotalcM); break;
                default:
                    q = _families.AsParallel().OrderBy(f => f.Surname); break;
            }
            _families = q.ToList();
        }
        public void AddFamilies(Match match) //string familyString, int matchID)
        {
            List<int> stack = new List<int>();
            string familyString = match.FamilyString;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            string name = "";
            var names = familyString.Split('/');
            foreach (string n in names)
            {
                name = n;
                int ndx = name.IndexOf('(');
                if (ndx >= 0)
                {
                    name = name.Substring(0, ndx);
                }
                name = name.Replace("'", "");
                name = name.Trim();
                ndx = name.IndexOf(" ");
                if (ndx > 0)
                {
                    name = name.Substring(0, ndx);
                }
                name = rgx.Replace(name, "");
                if (Regex.IsMatch(name.ToUpper(), @"^[a-zA-Z]+$")
                && name.Length > 2)
                {
                    AddFamily(name.ToLower(), match.ID, match.TotalGroupcM, stack);
                }
            }
            foreach (var f in stack)
            {
                _families[f].AddStack(stack);
            }
        }
        private void AddFamily(
            string surname,
            int matchID, double cM, List<int> idStack)
        {
            if (surname == null || surname.Length <= 2)
            {
                return;
            }
            var family = _families.FirstOrDefault(m => m.Surname == surname);
            if (family == null)
            {
                family = new Family(_families.Count, surname);
                _families.Add(family);
            }
            idStack.Add(family.ID);
            family.AddMatch(matchID, cM);
        }

        //public void AddcM(int matchID, double cM)
        //{
        //    var families = _families.FindAll(f => f.ContainsMatch(matchID));
        //    foreach (Family f in families)
        //    {
        //        f.AddcM(cM);
        //    }
        //}
        public void Complete()
        {
            foreach (var f in _families)
            {
                f.Complete();
            }
        }

        public void Output(StringBuilder sb, List<int> matchIDList)
        {
            var families = _families.FindAll(f => f.ContainsMatch(matchIDList));
            foreach (Family f in families)
            {
                sb.AppendLine(f.Surname + " : " + f.TotalcM + " : " + f.MatchCount + Environment.NewLine);
            }
        }

        public string Output(int matchID)
        {
            string output = Environment.NewLine;
            var families = _families.FindAll(f => f.ContainsMatch(matchID));
            foreach (Family f in families)
            {
                output += f.Surname + " : (" + f.TotalcM + "),";
            }
            return output + Environment.NewLine;
        }

        public string Output(StringBuilder sb, string path)
        {
            string output = Environment.NewLine;
            var oFamilies = _families.OrderByDescending(f => f.AverageCM).ToList();
            var cnt = 0;
            string p = path + @"\SurnameStacks.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (TextWriter writer = File.CreateText(p))
            {

                foreach (var f in oFamilies)
                {
                    output = f.Surname + " : (" + f.TotalcM + ")";
                    output += Environment.NewLine;
                    writer.Write(output);
                    var stack = f.OrderedStack;
                    output = "";
                    foreach (var s in stack)
                    {
                        output += _families[s.Key].Surname + ":";
                        output += s.Value + ",  ";
                    }
                    output += Environment.NewLine;
                    output += Environment.NewLine;
                    writer.Write(output);
                    cnt++;
                }
                writer.Close();
            }
            return output + Environment.NewLine;
        }

        public List<string> getSurname(int matchID)
        {
            List<string> surnames = new List<string>();
            var families = _families.FindAll(f => f.ContainsMatch(matchID));
            foreach (var f in families)
            {
                surnames.Add(f.Surname);
            }
            return surnames;
        }
    }
    public class Match
    {
        string _sourceID = "";
        int _ID = -1;
        List<string> _members = new List<string>();
        string _familyString = "";
        Dictionary<int, double> _groupcM = new Dictionary<int, double>();


        public Match(string sourceID, int id, string familyString)
        {
            _sourceID = sourceID.Trim();
            _ID = id;
            _familyString = familyString;
        }

        public int GroupCount
        {
            get
            {
                return _groupcM.Count();
            }
        }
        public bool MemberCompare(List<string> members)
        {// Find all that are common between the two lists...
            var result = _members.FindAll(m => members.IndexOf(m) < 0);
            return result.Count <= 0; // List 1 contains all of list 2...
        }
        public double TotalGroupcM
        {
            get { return _groupcM.Sum(g => g.Value); }
        }

        public void addGroup(int groupID, double cM)
        {
            if (!_groupcM.Keys.Contains(groupID))
            {
                _groupcM.Add(groupID, cM);
            }
        }

        public int ID
        {
            get { return _ID; }
        }
        public string SourceID
        {
            get { return _sourceID; }
        }


        public string FamilyString
        {
            get { return _familyString; }
        }
        public void AddMember(string memberID)
        {
            if (!_members.Contains(memberID))
            {
                _members.Add(memberID);
            }
        }

        public int MemberCount
        {
            get { return _members.Count; }
        }

        public bool MemberOf(string members)
        {
            if (members.Length <= 0) { return true; }
            int inclusiveResult = 0;
            int exclusiveResult = 0;
            string[] exclusiveList = members.Split('|');

            foreach (var m in exclusiveList)
            {
                if (_members.Contains(m.Trim()))
                {
                    exclusiveResult = 1;
                }
            }

            string[] inclusiveList = members.Split(null);

            foreach (var m in inclusiveList)
            {
                if (_members.Contains(m.Trim()))
                {
                    inclusiveResult += 1;
                }
            }

            return inclusiveResult == inclusiveList.Length || exclusiveResult > 0;
        }
        public string AllMembers
        {
            get
            {
                string result = "";

                foreach (var m in _members)
                {
                    result += m;
                    result += ",";
                }
                return result;
            }
        }

        public string Groups
        {
            get
            {
                string output = "";
                foreach (var k in _groupcM.Keys)
                {
                    output += k.ToString() + " : " + _groupcM[k].ToString();
                }
                return output;
            }
        }
    }

    public class DNAGroup
    {
        int _id = -1;
        string _memberID = "";
        int _chromosome = 0;
        List<string> _group;
        double _cM = 0.0;
        List<KeyValuePair<int, int>> _orderedPairs;

        Dictionary<int, int> _distance = new Dictionary<int, int>();
        public DNAGroup(int id, string memberID, int cid, List<string> group, double cM)
        {
            _id = id;
            _memberID = memberID;
            _chromosome = cid;
            _group = new List<string>(group);
            _cM = cM;
        }

        public int ID
        {
            get { return _id; }
        }
        public string MemberID
        {
            get { return _memberID; }
        }
        public List<string> group
        {
            get { return _group; }
        }
        public bool ContainsGroupMatch(string matchID)
        {
            return _group.Contains(matchID);
        }
        public int GroupCount
        {
            get { return _group.Count; }
        }

        public int GroupOverlap
        {
            get { return _orderedPairs.Count <= 0 ? 0 : _orderedPairs[0].Value; }
        }

        public void CompareGroup(DNAGroup group)
        {
            int matches = 0;
            foreach (var g in _group)
            {
                if (group.ContainsGroupMatch(g))
                {
                    matches += 1;
                }
            }
            if (matches > 1)
            {// how man common matches exist between groups...
                _distance.Add(group.ID, matches);
            }
        }
        public int Distance(int grpID)
        {
            if (_distance.ContainsKey(grpID))
            {
                return _distance[grpID];
            }
            return 0;
        }
        public bool InGroup(int gid)
        {
            return _distance.Keys.Contains(gid);
        }
        public void Complete()
        {
            _orderedPairs = _distance.OrderByDescending(d => d.Value).ToList();
        }

        public void Output(TextWriter w)
        {
            string output = _id.ToString() + " : ";
            output += Environment.NewLine;

            // is Xn in Xn-1
            foreach (var k in _orderedPairs)
            {
                output += k.Key + " : ";
                output += k.Value.ToString() + " : ";
                output += Environment.NewLine;
            }
            foreach (var g in _group)
            {
                output += g + ",";
            }
            output += Environment.NewLine;
            output += Environment.NewLine;

            w.WriteLine(output);
        }
    }

    public class Pair<T>
    {
        public Pair(T valuea, T valueb)
        {

        }

        public T ValueA { get; private set; }
        public T ValueB { get; private set; }


    }

    public class Pairs<T, U>
    {
        // GID/MID/
        // MID/SID

        List<Tuple<T, U>> _pairs = new List<Tuple<T, U>>();
        public Pairs()
        {

        }

        public void AddPair(T valueA, U valueB)
        {
            _pairs.Add(new Tuple<T, U>(valueA, valueB));
        }




        //  G/M   How many times does M group with other Ms
        //  List<Pair<T>> Pairs { get; }
    }
    public class Matches
    {
        Families _families = new Families();
        List<Match> _matches = new List<Match>();
        List<Match> _commonMatches = new List<Match>();
        List<DNAGroup> _groups = new List<DNAGroup>();

        //      Pairs<int, int> groupMembers = new Pairs<int, int>();
        //       Pairs<int, int> MemberFamilies = new Pairs<int, int>();
        List<Tuple<int, int>> groupMembers = new List<Tuple<int, int>>();
        List<Tuple<int, int>> memberFamilies = new List<Tuple<int, int>>();
        List<List<int>> groupContent;// How many groupA has in common with groupB
        List<Tuple<int, Tuple<int, int>>> matchGroups;// How often does matchA group with matchB.
        List<Tuple<int, Tuple<int, int>>> familyGroups;// How often does familyA group with familyB.

        //   List<Tuple<>>
        //

        //  List<int>
        // groupmatches
        public Matches()
        {
            //  groupMembers.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        }

        public void addGroup(string mid, int cid, List<string> group, double cM)
        {
            foreach (var g in group)
            {
                var match = _matches.FirstOrDefault(m => m.SourceID == g);
                if (null != match)
                {
                    match.addGroup(_groups.Count, cM);
                }
            }
            _groups.Add(new DNAGroup(_groups.Count, mid, cid, group, cM));
        }

        public void processGroupContent()
        {

        }
        public void AddMatch(string memberID, string sourceID, string families)
        {
            if (sourceID == null || sourceID.Length <= 0)
            {
                return;
            }
            var match = _matches.FirstOrDefault(m => m.SourceID == sourceID);
            if (match == null)
            {
                match = new Match(sourceID, _matches.Count, families);
                _matches.Add(match);
            }

            match.AddMember(memberID);
            //  _families.AddFamilies(families, match.ID);
        }
        public void Reset()
        {
            _matches.Clear();
            _commonMatches.Clear();
            _families = new Families();
        }
        public int Finish()
        {
            int cnt = _groups.Count - 1;
            for (int i = 0; i < cnt; i += 1)
            {
                for (int j = i + 1; j < cnt; j += 1)
                {
                    _groups[i].CompareGroup(_groups[j]);
                }
            }
            // groupContent
            _commonMatches = _matches.FindAll(m => m.GroupCount > 1).OrderBy(m => m.MemberCount).ToList();
            _families.Clear();
            var familyMatches = _commonMatches.Where(m => m.FamilyString.Length > 0).ToList();
            familyMatches.ForEach(m => _families.AddFamilies(m));
            _families.Complete();
            //  _families.Sort();
            _groups.ForEach(g => g.Complete());
            _groups.OrderBy(g => g.GroupOverlap);


            return _matches.Count;
        }

        public string Output(Segments segments, List<string> members, string path)
        {
            //   double cM;
            string line;
            StringBuilder output = new StringBuilder();

            var matches = _commonMatches.OrderByDescending(m => m.GroupCount);
            string p = path + @"\MatchResults.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
 

            StringBuilder familyOutput1 = new StringBuilder();
            List<int> added = new List<int>();
            groupContent = new List<List<int>>();
            // for each match
            // find all groups that contain that match
            // and put into a group 

            for (int i = 0; i < _groups.Count; i = i + 1)
            {// group all of the groups that have members in common...
                var iid = _groups[i].ID;
                if (added.Contains(iid) || groupContent.Any(g => g.IndexOf(iid)>= 0))
                {
                    continue;
                }
                added.Add(iid);
                for (int j = i + 1; j < _groups.Count; j = j + 1)
                {
                    var jid = _groups[j].ID;
                    if (added.Contains(jid) || groupContent.Any(g => g.IndexOf(jid) >= 0))
                    {
                        continue;
                    }
                    if (_groups[i].InGroup(jid))
                    {// If there is an overlap between jid and iid...
                        added.Add(jid);
                    }
                }
                if (added.Count > 0)
                {
                    groupContent.Add(added.GetRange(0,added.Count));
                    added.Clear();
                }
            }
            Dictionary<string, Dictionary<string, int>> allfamilies = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, int> families = new Dictionary<string, int>();
            groupContent = groupContent.FindAll(g => g.Count > 1).ToList();
            groupContent = groupContent.OrderByDescending(t => t.Count).ToList();
            using (TextWriter writer = File.CreateText(p))
            {
                int linecnt = 0;
                List<string> memberList = new List<string>();
                foreach (var g in groupContent)
                {
                    memberList.Clear();
                    writer.WriteLine("############################ " + g.Count.ToString() + Environment.NewLine);
                    writer.WriteLine(Environment.NewLine);
                    families.Clear();
                    foreach (var gid in g)
                    {
                        foreach (var m in _groups[gid].group)
                        {
                            var match = _matches.FirstOrDefault(mm => mm.SourceID == m);
                            if (null != match && match.MemberCount > 1)
                            {
                                if( memberList.IndexOf(match.SourceID)>= 0)
                                {
                                    continue;
                                }
                                if (match.MemberCompare(members))
                                {// No common matches...
                                    continue;
                                }

                                memberList.Add(match.SourceID);
                                List<string> names = _families.getSurname(match.ID);
                                foreach (string n in names)
                                {
                                    if (!families.ContainsKey(n))
                                    {
                                        families.Add(n, 0);
                                    }
                                    families[n] += 1;
                                }
                                //families.AddRange(names);
                                //  families.Add(_families.g)
                                line = match.SourceID;
                                line += "(" + match.GroupCount + ")";
                                line += " : ";
                                line += match.AllMembers;
                                writer.WriteLine(line);
                                writer.WriteLine(_families.Output(match.ID) + Environment.NewLine);
                                writer.WriteLine(Environment.NewLine);
                            }
                        }
                    }
                   

                    var fex = families.Where(k => k.Value > 1).ToList();
                    fex = fex.OrderByDescending(k => k.Value).ToList();
                    foreach( var f in fex)
                    {
                        writer.WriteLine(f.Key + "(" + f.Value + ") *, ");
                    }
                    foreach (var f in families.OrderBy(k => k.Key))
                    {
                        allfamilies.Add(f.Key, new Dictionary<string, int>());
                        writer.Write(f.Key + "(" + f.Value + ") *, ");
                    }
                    writer.WriteLine(Environment.NewLine);
                }
                writer.Close();
            }

            return "";
        }
    }
}
