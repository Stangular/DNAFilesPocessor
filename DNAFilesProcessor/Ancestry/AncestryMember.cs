using System.Collections.Generic;
using System.Xml.Linq;

namespace DNAFilesProcessor.Ancestry
{
  
    public class AncestryMember
    {
        public string AdminID { get; private set; }
        public bool ICWOnly { get; private set; }
        public List<string> ProjectMembers { get; } = new List<string>();
        public List<string> ICW { get; } = new List<string>(); 

        public AncestryMember(string id, bool icwonly = false, List<string> projectMembers = null)
        {
            AdminID = id;
            ICWOnly = icwonly;
            if (projectMembers != null)
            {
                ProjectMembers.AddRange(projectMembers);
            }
        }

        public int ProjectCount { get { return ProjectMembers.Count;  } }
        public void addICW(string id)
        {
            if( ICW.FindIndex( a => a == id) < 0)
            {
                ICW.Add(id);
            }
        }

        public XElement GetAsXML()
        {
            XElement project = new XElement("Admin",
                new XElement("ID", AdminID));
            foreach (var m in ProjectMembers)
            {
                project.Add(new XElement("PMember", m));
            }
            foreach (var w in ICW)
            {
                project.Add(new XElement("ICW", w));
            }
            return project;
        }
    }
}
