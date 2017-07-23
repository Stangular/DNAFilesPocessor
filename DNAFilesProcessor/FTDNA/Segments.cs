using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAFilesProcessor
{

    public class Segment
    {
        int _ID = -1;
        string _matchID = "";
        double _cM = 0.0;
        int _finalOffset = 0;
        int _firstOffset = 0;
        bool _primary = false;
        List<int> _groups = new List<int>();
        public Segment(int ID, string matchID, double cM, int finalOffset, int firstOffset)
        {
            _ID = ID;
            _matchID = matchID.Trim();
            _cM = cM;
            _finalOffset = finalOffset;
            _firstOffset = firstOffset;
        }

        public int ID
        {
            get { return _ID; }
        }
        public string MatchID
        {
            get { return _matchID; }
        }
        public double cM
        {
            get { return _cM; }
        }
        public int FirstOffset
        {
            get { return _firstOffset; }
        }
        public int FinalOffset
        {
            get { return _finalOffset; }
        }
        public bool Overlaps(Segment segment)
        {
            return !(segment.FirstOffset > _finalOffset || segment.FinalOffset < _firstOffset);
        }
        public bool Primary
        {
            get { return _primary; }
        }
        public void SetPrimary(SurnameProject project)
        {
            _primary = project.IsMember(_matchID);
        }

        public void AddGroup(int grpID)
        {
            _groups.Add(grpID);
        }
    }

    public class SegmentGroup
    {
        int _id = 0;
        List<int> _segments = new List<int>();

        public SegmentGroup(int id, List<int> segments)
        {
            _id = id;
            _segments.AddRange(segments);
        }
    }

    public class MatchDNAAssociation
    {
        int _matchID = -1;
        int _groupCount = 0;
        double _totalcM = 0.0;

        public MatchDNAAssociation(int matchID, int groupCount, double cM)
        {
            _matchID = matchID;
            _groupCount = groupCount;
            _totalcM = cM;
        }

        public int MatchID
        {
            get
            {
                return _matchID;
            }
        }

        public int GroupCount
        {
            get { return _groupCount; }
        }

        public double cM
        {
            get { return _totalcM; }
        }
    }
    public class Chromosome
    {
        List<Segment> _segments = new List<Segment>();

        public Chromosome()
        {

        }

        public bool AddSegment(string[] segmentData)
        {
            bool result = false;
            string matchID = segmentData[1];
            if (matchID.Length <= 0)
            {
                return false;
            }
            double cM = 0;
            int firstOffset = 0;
            int finalOffset = 0;
            if (!double.TryParse(segmentData[5], out cM) || cM < 3.0)
            {
                return false;
            }
            if (!int.TryParse(segmentData[3], out firstOffset))
            {
                return false;
            }
            if (!int.TryParse(segmentData[4], out finalOffset))
            {
                return false;
            }

            _segments.Add(new Segment(_segments.Count, matchID, cM, finalOffset, firstOffset));
            return result;
        }

        public bool AddSegment(string matchID, double cM, int finalOffset, int firstOffset)
        {
            _segments.Add(new Segment(_segments.Count, matchID, cM, finalOffset, firstOffset));
            return true;
        }

        //private void resolveGroupDistances(List<i> segmentGroups)
        //{
        //    int i = 0, len = _segmentGroups.Count, matchCount = 0;
        //    string sss = "";
        //    for(i=0; i < len; i = i + 1)
        //    {
        //        var gi = _segmentGroups[i];
        //        Dictionary<int, decimal> gd = new Dictionary<int, decimal>();
        //        for (int j = i + 1; j < len; j = j + 1)
        //        {
        //            var gj = _segmentGroups[j];
        //            matchCount = 0;
        //            foreach (int si in gi)
        //            {
        //                var segi = _segments[si];
        //                foreach ( int sj in gj)
        //                {
        //                    var segj = _segments[sj];

        //                    if( segi.MatchID == segj.MatchID)
        //                    {
        //                        matchCount++;
        //                    }
        //                }
        //            }
        //            sss += matchCount / gi.Count;
        //            sss += "  ";
        //            gd.Add(j, matchCount / gi.Count);
        //        }
        //        sss += Environment.NewLine;
        //        _groupDistance.Add(i, gd);
        //    }
        //}
        private bool findInGroup(int id, List<List<int>> segmentGroups)
        {
            return segmentGroups.Count(g => g.Contains(id)) > 0;
        }
        public void Complete(string memberID,int cid,Matches matches)
        {
            Complete(memberID, cid, matches,_segments.OrderBy(s => s.FirstOffset).ToList());
        }

        private double getOverlappingSegments(List<Segment> segments, List<string> group)
        {
            if (segments.Count < 2)
            {
                return 0.0;
            }
            group.Add(segments[0].MatchID);
            double cM = segments[0].cM;
            for (int j = 1; j < segments.Count; j += 1)
            {
                if (segments[0].Overlaps(segments[j]))
                {
                    cM += segments[j].cM;
                    group.Add(segments[j].MatchID);
                }
            }
            return cM;
        }
        private void Complete(string memberID, int cid, Matches matches, List<Segment> segments)
        {
            List<string> group = new List<string>();
            double cM = getOverlappingSegments(segments, group);
            if (group.Count > 1)
            {
                matches.addGroup(memberID, cid, group, cM);
                foreach (var m in group)
                {
                    segments.Remove(segments.FirstOrDefault(s => s.MatchID == m));
                }
                Complete(memberID, cid, matches, segments);
            }
        }
        public double GetMatchCM(Match match, List<List<int>> segmentGroups)
        {
            double totalcM = 0;
            var segments = _segments.FindAll(s => s.MatchID == match.SourceID);
            foreach (var s in segments)
            {
                var group = segmentGroups.FirstOrDefault(g => g.IndexOf(s.ID) >= 0);
                if (null == group) { continue; }
                foreach (var segmentID in group)
                {
                    var segment = _segments.FirstOrDefault(seg => seg.ID == segmentID);
                    if (null != segment)
                    {
                        totalcM += segment.cM;
                    }
                }
            }
            return totalcM;
        }
        public double DNAAssociation(List<int> segments)
        {
            double totalcM = 0;
            foreach (var sid in segments)
            {
                var segment = _segments.FirstOrDefault(seg => seg.ID == sid);
                if (null != segment)
                {
                    totalcM += segment.cM;
                }
            }
            return totalcM;
        }
    }
    public class MemberSegments
    {
        string _memberID = "";
        string _memberMatchName = "";
        List<Chromosome> _chromosomes = new List<Chromosome>();
        //   List<MatchDNAAssociation> _MatchingDNA = new List<MatchDNAAssociation>();
        public MemberSegments(string memberID, string memberMatchName)
        {
            _memberID = memberID;
            _memberMatchName = memberMatchName;
            for (int i = 0; i < 23; i = i + 1)
            {
                _chromosomes.Add(new Chromosome());
            }
        }
        public string MemberID
        {
            get { return _memberID; }
        }

        public double totalcM(Match match, List<List<int>> segmentGroups)
        {
            double totalcM = 0.0;

            foreach (var c in _chromosomes)
            {
                totalcM += c.GetMatchCM(match, segmentGroups);
            }

            return totalcM;
        }
        public string MemberMatchName
        {
            get { return _memberMatchName; }
        }

        public bool AddSegment(string[] segmentData)
        {
            bool result = false;
            if (segmentData.Length != 7) { return result; }
            int c = 1;
            if (int.TryParse(segmentData[2], out c))
            {
                _chromosomes[c - 1].AddSegment(segmentData);
                result = true;
            }
            return result;
        }
        public void AddSegment(int chromosome, string matchID, double cM, int finalOffset, int firstOffset)
        {
            _chromosomes[chromosome].AddSegment(matchID, cM, finalOffset, firstOffset);
        }

        public void Complete(Matches matches)
        {
            for (int i = 0; i < 23; i = i + 1)
            {
                _chromosomes[i].Complete( _memberID,i,matches);
            }
        }
    }

    public class Segments
    {
        List<List<int>> _segmentGroups = new List<List<int>>();
        List<MemberSegments> _segments = new List<MemberSegments>();
        int _selectedSegment = -1;
        public Segments()
        {

        }

        public void Reset()
        {
            _segments.Clear();
            _selectedSegment = -1;
        }
        public void AddNewMemberSegment(string memberID, string primaryID)
        {
            var segment = _segments.FirstOrDefault(s => s.MemberID == memberID);
            if (null != segment) { return; }
            _selectedSegment = _segments.Count;
            _segments.Add(new MemberSegments(memberID, primaryID));
        }
        public void AddSegment(string[] segmentData)
        {
            if (_selectedSegment >= 0)
            {
                _segments[_selectedSegment].AddSegment(segmentData);
            }
        }
        public int Finish(Matches matches)
        {
            foreach (var s in _segments)
            {
                s.Complete(matches);
            }
            _segmentGroups = _segmentGroups.OrderBy(g => g.Count).ToList();

            // Attach families to segments
            // 1) Which families belong to which segment (groups?)
            // 2) Ration of shared families to single families per match. (a match has one family that is shared 9 families that are not shared 9/1 is weight given to that family. Match should
            // be flagged.
            return _segments.Count;
        }
    }

}
