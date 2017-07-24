using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNAFilesProcessor
{
    public partial class DNAParser : Form
    {

        bool _familyMemberCountSort = false;
        bool _familySurnameSort = true;
        Projects _projects = new Projects();
        List<string> _processingItems = new List<string>();
        public DNAParser()
        {
            InitializeComponent();

            comboBox_process_member.SelectedIndex = -1;
            comboBox_filetype.Items.Add("FTDNA Matches");
            comboBox_filetype.Items.Add("FTDNA DNA Segments");
            comboBox_filetype.Items.Add("Ancestry Matches");
            comboBox_filetype.Items.Add("Ancestry ICW");
            comboBox_filetype.Items.Add("Ancestry Trees");

            _projects.mProgessDelegate = new ProgessUpdateDelegate(this.UpdateProgressBar);
            _projects.mFileProgressDelegate = new FileProcessingUpdateDelegate(this.UpdateProcessingList);
        }

        private void button_parse_file_Click(object sender, EventArgs e)
        {
            string[] primaryMembers = textBox_primary_member_list.Text.Split(',');
            string[] secondaryMembers = textBox_secondary_member_list.Text.Split(',');
            _projects.ProcessMembers(primaryMembers.Concat(secondaryMembers).ToList());

            //  BindingSource bsA = new BindingSource();

            //    listView_ancestry_family_results.DataBindings.Add( = _projects.CommonFamilies.Families;
        }

        private void UpdateProgressBar(long currentProgress)
        {
            if (!InvokeRequired)
            {
                progressBar_fileparsing.Value = (int)currentProgress;
            }
            else
            {
                Invoke(new Action<long>(UpdateProgressBar), currentProgress);
            }
        }

        private void UpdateProcessingList(string processingMessage)
        {
            if (!InvokeRequired)
            {
                string code = processingMessage.Substring(0, 1);
                processingMessage = processingMessage.Substring(2);
                switch (code)
                {
                    case "0":
                        _processingItems.Add(processingMessage + " processing...");
                        break;
                    case "2":
                        _processingItems.RemoveAt(_processingItems.Count - 1);
                        _processingItems.Add(processingMessage + " complete.");
                        break;
                }
                listBox_processed_files.DataSource = null;
                listBox_processed_files.DataSource = _processingItems;
            }
            else
            {
                Invoke(new Action<string>(UpdateProcessingList), processingMessage);
            }
        }

        private void button_complete_Click(object sender, EventArgs e)
        {
            _projects.Complete();
        }

        private void button_load_file_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                DialogResult r = dlg.ShowDialog();
                if (r == DialogResult.OK)
                {
                    string p = dlg.SelectedPath;
                    if (!System.IO.Directory.Exists(p))
                    {
                        System.IO.Directory.CreateDirectory(p);
                    }
                    textBox_output_path.Text = p;
                }
            }
        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_primary_member_list.Text = "";
        }

        private void button_clear_secondary_Click(object sender, EventArgs e)
        {
            textBox_secondary_member_list.Text = "";
        }

        private void tableLayoutPanel_results_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This will remove all processed results.  Do you wish to continue?", "Remove Results", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                _projects.Reset();
                textBox_primary_member_list.Text = "";
                textBox_secondary_member_list.Text = "";
            }
        }

        private void button_output_Click(object sender, EventArgs e)
        {
            string[] secondaryMembers = textBox_secondary_member_list.Text.Split(',');
            _projects.Output(secondaryMembers, textBox_output_path.Text);
        }


        private void listView_selected_files_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_add_project_Click(object sender, EventArgs e)
        {
            string newSurname = comboBox_surname_projects.Text;
            if (newSurname.Length <= 0)
            {
                MessageBox.Show("No Surname listed for project");
                return;
            }
            if (_projects.ContainsProject(newSurname))
            {
                MessageBox.Show("A project already exists for surname " + newSurname);
                return;
            }
            _projects.AddProject(newSurname);
        }

        private void button_add_member_Click(object sender, EventArgs e)
        {
            if (!_projects.ValidProject)
            {
                MessageBox.Show("Please select or create a Surname project before adding members.");
                return;
            }
            string memberID = comboBox_project_members.Text;
            if (!_projects.Project.AddMember(memberID))
            {
                MessageBox.Show(memberID + " is not a valid member id. Please use another ID");
            }
            textBox_member_name.Text = "";
            textBox_member_notes.Text = "";
            listView_member_files.Items.Clear();

        }

        private void button_add_file_Click(object sender, EventArgs e)
        {
            if (_projects.Project.Member.MemberID.Length <= 0)
            {
                MessageBox.Show("No member selected.");
                return;
            }
            if (comboBox_filetype.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a file type.");
                return;
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                DialogResult r = dlg.ShowDialog();
                if (r == DialogResult.OK)
                {
                    listView_member_files.View = View.Details;
                    string fileType = comboBox_filetype.Text;
                    string fileName = System.IO.Path.GetFileName(dlg.FileName);
                    listView_member_files.Items.Add(new ListViewItem(new string[] { fileType, fileName }));
                    _projects.Project.Member.AddDNAFile(comboBox_filetype.SelectedIndex, dlg.FileName);

                }
            }
        }

        private void button_update_member_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_projects.Project.Member.MemberID))
            {
                MessageBox.Show("No member selected.");
                return;
            }
            string memberName = textBox_member_name.Text;
            if (String.IsNullOrWhiteSpace(memberName))
            {
                MessageBox.Show("No member name defined.");
                return;
            }

            _projects.Project.Member.Update(memberName, textBox_member_notes.Text);
        }

        private void button_clear_files_Click(object sender, EventArgs e)
        {
            if (!_projects.ValidProject)
            {
                MessageBox.Show("No project selected");
                return;
            }
            _projects.Project.Member.ClearFiles();
            listView_member_files.Items.Clear();
        }

        private void button_remove_member_Click(object sender, EventArgs e)
        {

        }

        private void button_remove_project_Click(object sender, EventArgs e)
        {

        }

        private void button_save_project_Click(object sender, EventArgs e)
        {
            string filename = "";
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                // dlg.Multiselect = true;
                DialogResult r = dlg.ShowDialog();
                if (r == DialogResult.OK)
                {
                    if (!dlg.CheckFileExists)
                    {
                        System.IO.FileStream strm = System.IO.File.Create(dlg.FileName);
                        strm.Close();
                    }
                    filename = dlg.FileName;
                }
            }
            _projects.Save(filename);

        }

        private void button_load_projects_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (Directory.Exists(@"C:\DNAOutput"))
                {
                    dlg.InitialDirectory = @"C:\DNAOutput";
                }
                else
                {
                    dlg.InitialDirectory = @"C:\";
                }
                DialogResult r = dlg.ShowDialog();
                if (r == DialogResult.OK)
                {
                    _projects.OpenProjects(dlg.FileName);
                    _projects.UI(comboBox_surname_projects);

                }
            }
        }

        private void comboBox_surname_projects_SelectedIndexChanged(object sender, EventArgs e)
        {
            _projects.SelectProject(comboBox_surname_projects.Text);
            _projects.Project.UI(comboBox_project_members);
            _projects.Project.UI(comboBox_process_member);
        }

        private void comboBox_project_members_SelectedIndexChanged(object sender, EventArgs e)
        {
            _projects.Project.SelectMember(comboBox_project_members.Text);
            textBox_member_name.Text = _projects.Project.Member.Name;
            textBox_member_notes.Text = _projects.Project.Member.Notes;
            listView_member_files.Items.Clear();
            listView_member_files.View = View.Details;
            foreach (var f in _projects.Project.Member.Files)
            {
                var fileType = comboBox_filetype.Items[f.FileType];
                listView_member_files.Items.Add(
                    new ListViewItem(new string[] { fileType.ToString(), f.FileName }));

            }

        }

        private void button_add_primary_Click(object sender, EventArgs e)
        {
            string t = comboBox_process_member.Text;
            if (String.IsNullOrWhiteSpace(t))
            {
                MessageBox.Show("Invalid Member ID");
                return;
            }
            if (textBox_primary_member_list.Text.IndexOf(t) >= 0
            || textBox_secondary_member_list.Text.IndexOf(t) >= 0)
            {
                MessageBox.Show("Member ID already included in list");
                return;

            }
            if (textBox_primary_member_list.Text.Length > 0)
            {
                textBox_primary_member_list.Text += ",";
            }
            textBox_primary_member_list.Text += t;
        }

        private void button_add_secondary_Click(object sender, EventArgs e)
        {
            string t = comboBox_process_member.Text;
            if (String.IsNullOrWhiteSpace(t))
            {
                MessageBox.Show("Invalid Member ID");
                return;
            }
            if (textBox_primary_member_list.Text.IndexOf(t) >= 0
            || textBox_secondary_member_list.Text.IndexOf(t) >= 0)
            {
                MessageBox.Show("Member ID already included in list");
                return;
            }
            if (textBox_secondary_member_list.Text.Length > 0)
            {
                textBox_secondary_member_list.Text += ",";
            }
            textBox_secondary_member_list.Text += t;
        }

        private void comboBox_filetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl_Process_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_Process.SelectedIndex == 2)
            {
                Init();
            }
        }

        private void dataGridView_families_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    dataGridView_families.DataSource = null;
                    _familySurnameSort = !_familySurnameSort;
                    if (_familySurnameSort)
                    {
                        dataGridView_families.DataSource = _projects.CommonFamilies.Families.OrderBy(f => f.Surname).ToList();
                    }
                    else
                    {
                        dataGridView_families.DataSource = _projects.CommonFamilies.Families.OrderByDescending(f => f.Surname).ToList();
                    }
                    break;
                case 1:
                    dataGridView_families.DataSource = null;
                    _familyMemberCountSort = !_familyMemberCountSort;
                    if (_familyMemberCountSort)
                    {
                        dataGridView_families.DataSource = _projects.CommonFamilies.Families.OrderByDescending(f => f.MemberCount).ToList();
                    }
                    else
                    {
                        dataGridView_families.DataSource = _projects.CommonFamilies.Families.OrderBy(f => f.MemberCount).ToList();
                    }
                    break;
            }
        }

        private void dataGridView_families_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var family = (Ancestry.AncestryFamily)dataGridView_families.SelectedRows[0].DataBoundItem;
            dataGridView_family_members.DataSource = null;
            dataGridView_admin.DataSource = null;
            dataGridView_family_members.DataSource = family.Members.OrderByDescending( m => m.BirthYear ).ToList();
            dataGridView_admin.DataSource = _projects.MemberAdmins.Where(a => family.IncludesAdmin( a.AdminID )).ToList();
        }

        private void button_reset_results_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            dataGridView_families.DataSource = _projects.CommonFamilies.FamiliesForAdmin();
            dataGridView_families.AutoGenerateColumns = true;

            dataGridView_admin.DataSource = _projects.MemberAdmins.OrderBy( m => m.AdminID).ToList();
            dataGridView_admin.AutoGenerateColumns = true;
            dataGridView_family_members.DataSource = null;
        }

        private void dataGridView_family_members_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var familyMember = (Ancestry.AncestryFamilyMember)dataGridView_family_members.SelectedRows[0].DataBoundItem;

            dataGridView_families.DataSource = null;
            dataGridView_families.DataSource = _projects.CommonFamilies.FamiliesForAdmin(familyMember.Admins).ToList();
        }

        private void dataGridView_admin_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var admin = (Ancestry.AncestryMember)dataGridView_admin.SelectedRows[0].DataBoundItem;
            // get all families with this admin...
            dataGridView_families.DataSource = null;
            dataGridView_families.DataSource = _projects.CommonFamilies.FamiliesForAdmin(admin.AdminID).ToList();
        }

        private void button_save_ancestry_results_Click(object sender, EventArgs e)
        {
            string filename = "";
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                // dlg.Multiselect = true;
                DialogResult r = dlg.ShowDialog();
                if (r == DialogResult.OK)
                {
                    if (!dlg.CheckFileExists)
                    {
                        System.IO.FileStream strm = System.IO.File.Create(dlg.FileName);
                        strm.Close();
                    }
                    filename = dlg.FileName;
                }
            }
            _projects.SaveAncestry(filename);
        }
    }
}
