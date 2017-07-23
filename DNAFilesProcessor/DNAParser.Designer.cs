namespace DNAFilesProcessor
{
    partial class DNAParser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_load_file = new System.Windows.Forms.Button();
            this.button_parse_file = new System.Windows.Forms.Button();
            this.label_project_member = new System.Windows.Forms.Label();
            this.comboBox_process_member = new System.Windows.Forms.ComboBox();
            this.comboBox_filetype = new System.Windows.Forms.ComboBox();
            this.progressBar_fileparsing = new System.Windows.Forms.ProgressBar();
            this.button_complete = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.textBox_primary_member_list = new System.Windows.Forms.TextBox();
            this.button_output = new System.Windows.Forms.Button();
            this.textBox_secondary_member_list = new System.Windows.Forms.TextBox();
            this.tabControl_Process = new System.Windows.Forms.TabControl();
            this.tabPage_members = new System.Windows.Forms.TabPage();
            this.button_load_projects = new System.Windows.Forms.Button();
            this.button_save_project = new System.Windows.Forms.Button();
            this.button_remove_project = new System.Windows.Forms.Button();
            this.button_remove_member = new System.Windows.Forms.Button();
            this.button_clear_files = new System.Windows.Forms.Button();
            this.listView_member_files = new System.Windows.Forms.ListView();
            this.fileTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox_member_notes = new System.Windows.Forms.TextBox();
            this.label_member_notes = new System.Windows.Forms.Label();
            this.label_member_name = new System.Windows.Forms.Label();
            this.button_add_file = new System.Windows.Forms.Button();
            this.button_update_member = new System.Windows.Forms.Button();
            this.textBox_member_name = new System.Windows.Forms.TextBox();
            this.button_add_member = new System.Windows.Forms.Button();
            this.comboBox_project_members = new System.Windows.Forms.ComboBox();
            this.button_add_project = new System.Windows.Forms.Button();
            this.comboBox_surname_projects = new System.Windows.Forms.ComboBox();
            this.tabPage_process = new System.Windows.Forms.TabPage();
            this.button_clear_secondary = new System.Windows.Forms.Button();
            this.button_add_secondary = new System.Windows.Forms.Button();
            this.button_add_primary = new System.Windows.Forms.Button();
            this.label__secondary = new System.Windows.Forms.Label();
            this.label_primary = new System.Windows.Forms.Label();
            this.textBox_output_path = new System.Windows.Forms.TextBox();
            this.tabPage_results = new System.Windows.Forms.TabPage();
            this.listBox_processed_files = new System.Windows.Forms.ListBox();
            this.dataGridView_families = new System.Windows.Forms.DataGridView();
            this.Members = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_family_members = new System.Windows.Forms.DataGridView();
            this.dataGridView_admin = new System.Windows.Forms.DataGridView();
            this.Projects = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Admins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_reset_results = new System.Windows.Forms.Button();
            this.button_save_ancestry_results = new System.Windows.Forms.Button();
            this.label_projects = new System.Windows.Forms.Label();
            this.label_members = new System.Windows.Forms.Label();
            this.label_files = new System.Windows.Forms.Label();
            this.label_famlies = new System.Windows.Forms.Label();
            this.label_family_members = new System.Windows.Forms.Label();
            this.label_admin = new System.Windows.Forms.Label();
            this.adminIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iCWOnlyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ancestryMemberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commonDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ancestryFamilyMemberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ancestryFamilyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ancestryFamilyBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl_Process.SuspendLayout();
            this.tabPage_members.SuspendLayout();
            this.tabPage_process.SuspendLayout();
            this.tabPage_results.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_families)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_family_members)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_admin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryMemberBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryFamilyMemberBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryFamilyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryFamilyBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_load_file
            // 
            this.button_load_file.Location = new System.Drawing.Point(0, 365);
            this.button_load_file.Name = "button_load_file";
            this.button_load_file.Size = new System.Drawing.Size(112, 23);
            this.button_load_file.TabIndex = 0;
            this.button_load_file.Text = "Select Output Path";
            this.button_load_file.UseVisualStyleBackColor = true;
            this.button_load_file.Click += new System.EventHandler(this.button_load_file_Click);
            // 
            // button_parse_file
            // 
            this.button_parse_file.Location = new System.Drawing.Point(3, 128);
            this.button_parse_file.Name = "button_parse_file";
            this.button_parse_file.Size = new System.Drawing.Size(75, 23);
            this.button_parse_file.TabIndex = 2;
            this.button_parse_file.Text = "Parse Files";
            this.button_parse_file.UseVisualStyleBackColor = true;
            this.button_parse_file.Click += new System.EventHandler(this.button_parse_file_Click);
            // 
            // label_project_member
            // 
            this.label_project_member.AutoSize = true;
            this.label_project_member.Location = new System.Drawing.Point(6, 5);
            this.label_project_member.Name = "label_project_member";
            this.label_project_member.Size = new System.Drawing.Size(45, 13);
            this.label_project_member.TabIndex = 4;
            this.label_project_member.Text = "Member";
            // 
            // comboBox_process_member
            // 
            this.comboBox_process_member.FormattingEnabled = true;
            this.comboBox_process_member.Location = new System.Drawing.Point(6, 21);
            this.comboBox_process_member.Name = "comboBox_process_member";
            this.comboBox_process_member.Size = new System.Drawing.Size(127, 21);
            this.comboBox_process_member.TabIndex = 6;
            // 
            // comboBox_filetype
            // 
            this.comboBox_filetype.FormattingEnabled = true;
            this.comboBox_filetype.Location = new System.Drawing.Point(0, 116);
            this.comboBox_filetype.Name = "comboBox_filetype";
            this.comboBox_filetype.Size = new System.Drawing.Size(191, 21);
            this.comboBox_filetype.TabIndex = 8;
            this.comboBox_filetype.SelectedIndexChanged += new System.EventHandler(this.comboBox_filetype_SelectedIndexChanged);
            // 
            // progressBar_fileparsing
            // 
            this.progressBar_fileparsing.AccessibleRole = System.Windows.Forms.AccessibleRole.Alert;
            this.progressBar_fileparsing.Location = new System.Drawing.Point(0, 157);
            this.progressBar_fileparsing.Name = "progressBar_fileparsing";
            this.progressBar_fileparsing.Size = new System.Drawing.Size(567, 23);
            this.progressBar_fileparsing.TabIndex = 9;
            // 
            // button_complete
            // 
            this.button_complete.Location = new System.Drawing.Point(84, 129);
            this.button_complete.Name = "button_complete";
            this.button_complete.Size = new System.Drawing.Size(63, 22);
            this.button_complete.TabIndex = 0;
            this.button_complete.Text = "Complete";
            this.button_complete.UseVisualStyleBackColor = true;
            this.button_complete.Click += new System.EventHandler(this.button_complete_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(516, 58);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(66, 22);
            this.button_clear.TabIndex = 17;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(153, 128);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(71, 23);
            this.button_reset.TabIndex = 19;
            this.button_reset.Text = "Reset";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // textBox_primary_member_list
            // 
            this.textBox_primary_member_list.Location = new System.Drawing.Point(6, 60);
            this.textBox_primary_member_list.Name = "textBox_primary_member_list";
            this.textBox_primary_member_list.Size = new System.Drawing.Size(493, 20);
            this.textBox_primary_member_list.TabIndex = 20;
            // 
            // button_output
            // 
            this.button_output.Location = new System.Drawing.Point(496, 365);
            this.button_output.Name = "button_output";
            this.button_output.Size = new System.Drawing.Size(71, 23);
            this.button_output.TabIndex = 21;
            this.button_output.Text = "Output";
            this.button_output.UseVisualStyleBackColor = true;
            this.button_output.Click += new System.EventHandler(this.button_output_Click);
            // 
            // textBox_secondary_member_list
            // 
            this.textBox_secondary_member_list.Location = new System.Drawing.Point(6, 102);
            this.textBox_secondary_member_list.Name = "textBox_secondary_member_list";
            this.textBox_secondary_member_list.Size = new System.Drawing.Size(493, 20);
            this.textBox_secondary_member_list.TabIndex = 23;
            // 
            // tabControl_Process
            // 
            this.tabControl_Process.Controls.Add(this.tabPage_members);
            this.tabControl_Process.Controls.Add(this.tabPage_process);
            this.tabControl_Process.Controls.Add(this.tabPage_results);
            this.tabControl_Process.Location = new System.Drawing.Point(2, -2);
            this.tabControl_Process.Name = "tabControl_Process";
            this.tabControl_Process.SelectedIndex = 0;
            this.tabControl_Process.Size = new System.Drawing.Size(891, 673);
            this.tabControl_Process.TabIndex = 24;
            this.tabControl_Process.SelectedIndexChanged += new System.EventHandler(this.tabControl_Process_SelectedIndexChanged);
            // 
            // tabPage_members
            // 
            this.tabPage_members.Controls.Add(this.label_files);
            this.tabPage_members.Controls.Add(this.label_members);
            this.tabPage_members.Controls.Add(this.label_projects);
            this.tabPage_members.Controls.Add(this.button_load_projects);
            this.tabPage_members.Controls.Add(this.button_save_project);
            this.tabPage_members.Controls.Add(this.button_remove_project);
            this.tabPage_members.Controls.Add(this.button_remove_member);
            this.tabPage_members.Controls.Add(this.button_clear_files);
            this.tabPage_members.Controls.Add(this.listView_member_files);
            this.tabPage_members.Controls.Add(this.textBox_member_notes);
            this.tabPage_members.Controls.Add(this.label_member_notes);
            this.tabPage_members.Controls.Add(this.label_member_name);
            this.tabPage_members.Controls.Add(this.comboBox_filetype);
            this.tabPage_members.Controls.Add(this.button_add_file);
            this.tabPage_members.Controls.Add(this.button_update_member);
            this.tabPage_members.Controls.Add(this.textBox_member_name);
            this.tabPage_members.Controls.Add(this.button_add_member);
            this.tabPage_members.Controls.Add(this.comboBox_project_members);
            this.tabPage_members.Controls.Add(this.button_add_project);
            this.tabPage_members.Controls.Add(this.comboBox_surname_projects);
            this.tabPage_members.Location = new System.Drawing.Point(4, 22);
            this.tabPage_members.Name = "tabPage_members";
            this.tabPage_members.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_members.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage_members.Size = new System.Drawing.Size(883, 647);
            this.tabPage_members.TabIndex = 1;
            this.tabPage_members.Text = "Members";
            this.tabPage_members.UseVisualStyleBackColor = true;
            // 
            // button_load_projects
            // 
            this.button_load_projects.Location = new System.Drawing.Point(3, 19);
            this.button_load_projects.Name = "button_load_projects";
            this.button_load_projects.Size = new System.Drawing.Size(72, 23);
            this.button_load_projects.TabIndex = 28;
            this.button_load_projects.Text = "Open";
            this.button_load_projects.UseVisualStyleBackColor = true;
            this.button_load_projects.Click += new System.EventHandler(this.button_load_projects_Click);
            // 
            // button_save_project
            // 
            this.button_save_project.Location = new System.Drawing.Point(81, 19);
            this.button_save_project.Name = "button_save_project";
            this.button_save_project.Size = new System.Drawing.Size(75, 23);
            this.button_save_project.TabIndex = 27;
            this.button_save_project.Text = "Save";
            this.button_save_project.UseVisualStyleBackColor = true;
            this.button_save_project.Click += new System.EventHandler(this.button_save_project_Click);
            // 
            // button_remove_project
            // 
            this.button_remove_project.Location = new System.Drawing.Point(225, 57);
            this.button_remove_project.Name = "button_remove_project";
            this.button_remove_project.Size = new System.Drawing.Size(23, 23);
            this.button_remove_project.TabIndex = 26;
            this.button_remove_project.Text = "-";
            this.button_remove_project.UseVisualStyleBackColor = true;
            this.button_remove_project.Click += new System.EventHandler(this.button_remove_project_Click);
            // 
            // button_remove_member
            // 
            this.button_remove_member.Location = new System.Drawing.Point(488, 33);
            this.button_remove_member.Name = "button_remove_member";
            this.button_remove_member.Size = new System.Drawing.Size(22, 23);
            this.button_remove_member.TabIndex = 24;
            this.button_remove_member.Text = "-";
            this.button_remove_member.UseVisualStyleBackColor = true;
            this.button_remove_member.Click += new System.EventHandler(this.button_remove_member_Click);
            // 
            // button_clear_files
            // 
            this.button_clear_files.Location = new System.Drawing.Point(225, 114);
            this.button_clear_files.Name = "button_clear_files";
            this.button_clear_files.Size = new System.Drawing.Size(22, 23);
            this.button_clear_files.TabIndex = 22;
            this.button_clear_files.Text = "-";
            this.button_clear_files.UseVisualStyleBackColor = true;
            this.button_clear_files.Click += new System.EventHandler(this.button_clear_files_Click);
            // 
            // listView_member_files
            // 
            this.listView_member_files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileTypeHeader,
            this.fileNameHeader});
            this.listView_member_files.GridLines = true;
            this.listView_member_files.Location = new System.Drawing.Point(0, 143);
            this.listView_member_files.Name = "listView_member_files";
            this.listView_member_files.Size = new System.Drawing.Size(247, 411);
            this.listView_member_files.TabIndex = 21;
            this.listView_member_files.UseCompatibleStateImageBehavior = false;
            // 
            // fileTypeHeader
            // 
            this.fileTypeHeader.Text = "File Type";
            this.fileTypeHeader.Width = 100;
            // 
            // fileNameHeader
            // 
            this.fileNameHeader.Text = "File Name";
            this.fileNameHeader.Width = 470;
            // 
            // textBox_member_notes
            // 
            this.textBox_member_notes.Location = new System.Drawing.Point(294, 73);
            this.textBox_member_notes.Multiline = true;
            this.textBox_member_notes.Name = "textBox_member_notes";
            this.textBox_member_notes.Size = new System.Drawing.Size(463, 101);
            this.textBox_member_notes.TabIndex = 20;
            // 
            // label_member_notes
            // 
            this.label_member_notes.AutoSize = true;
            this.label_member_notes.Location = new System.Drawing.Point(291, 57);
            this.label_member_notes.Name = "label_member_notes";
            this.label_member_notes.Size = new System.Drawing.Size(76, 13);
            this.label_member_notes.TabIndex = 19;
            this.label_member_notes.Text = "Member Notes";
            // 
            // label_member_name
            // 
            this.label_member_name.AutoSize = true;
            this.label_member_name.Location = new System.Drawing.Point(516, 20);
            this.label_member_name.Name = "label_member_name";
            this.label_member_name.Size = new System.Drawing.Size(79, 13);
            this.label_member_name.TabIndex = 18;
            this.label_member_name.Text = "Member Name:";
            // 
            // button_add_file
            // 
            this.button_add_file.Location = new System.Drawing.Point(197, 114);
            this.button_add_file.Name = "button_add_file";
            this.button_add_file.Size = new System.Drawing.Size(22, 23);
            this.button_add_file.TabIndex = 16;
            this.button_add_file.Text = "+";
            this.button_add_file.UseVisualStyleBackColor = true;
            this.button_add_file.Click += new System.EventHandler(this.button_add_file_Click);
            // 
            // button_update_member
            // 
            this.button_update_member.Location = new System.Drawing.Point(713, 33);
            this.button_update_member.Name = "button_update_member";
            this.button_update_member.Size = new System.Drawing.Size(20, 23);
            this.button_update_member.TabIndex = 15;
            this.button_update_member.Text = "+";
            this.button_update_member.UseVisualStyleBackColor = true;
            this.button_update_member.Click += new System.EventHandler(this.button_update_member_Click);
            // 
            // textBox_member_name
            // 
            this.textBox_member_name.Location = new System.Drawing.Point(516, 33);
            this.textBox_member_name.Name = "textBox_member_name";
            this.textBox_member_name.Size = new System.Drawing.Size(191, 20);
            this.textBox_member_name.TabIndex = 13;
            // 
            // button_add_member
            // 
            this.button_add_member.Location = new System.Drawing.Point(459, 33);
            this.button_add_member.Name = "button_add_member";
            this.button_add_member.Size = new System.Drawing.Size(23, 23);
            this.button_add_member.TabIndex = 10;
            this.button_add_member.Text = "+";
            this.button_add_member.UseVisualStyleBackColor = true;
            this.button_add_member.Click += new System.EventHandler(this.button_add_member_Click);
            // 
            // comboBox_project_members
            // 
            this.comboBox_project_members.FormattingEnabled = true;
            this.comboBox_project_members.Location = new System.Drawing.Point(294, 33);
            this.comboBox_project_members.Name = "comboBox_project_members";
            this.comboBox_project_members.Size = new System.Drawing.Size(159, 21);
            this.comboBox_project_members.TabIndex = 9;
            this.comboBox_project_members.SelectedIndexChanged += new System.EventHandler(this.comboBox_project_members_SelectedIndexChanged);
            // 
            // button_add_project
            // 
            this.button_add_project.Location = new System.Drawing.Point(197, 57);
            this.button_add_project.Name = "button_add_project";
            this.button_add_project.Size = new System.Drawing.Size(22, 23);
            this.button_add_project.TabIndex = 8;
            this.button_add_project.Text = "+";
            this.button_add_project.UseVisualStyleBackColor = true;
            this.button_add_project.Click += new System.EventHandler(this.button_add_project_Click);
            // 
            // comboBox_surname_projects
            // 
            this.comboBox_surname_projects.FormattingEnabled = true;
            this.comboBox_surname_projects.Location = new System.Drawing.Point(0, 58);
            this.comboBox_surname_projects.Name = "comboBox_surname_projects";
            this.comboBox_surname_projects.Size = new System.Drawing.Size(191, 21);
            this.comboBox_surname_projects.TabIndex = 7;
            this.comboBox_surname_projects.SelectedIndexChanged += new System.EventHandler(this.comboBox_surname_projects_SelectedIndexChanged);
            // 
            // tabPage_process
            // 
            this.tabPage_process.Controls.Add(this.listBox_processed_files);
            this.tabPage_process.Controls.Add(this.button_clear_secondary);
            this.tabPage_process.Controls.Add(this.button_add_secondary);
            this.tabPage_process.Controls.Add(this.button_add_primary);
            this.tabPage_process.Controls.Add(this.label__secondary);
            this.tabPage_process.Controls.Add(this.label_primary);
            this.tabPage_process.Controls.Add(this.textBox_output_path);
            this.tabPage_process.Controls.Add(this.textBox_secondary_member_list);
            this.tabPage_process.Controls.Add(this.comboBox_process_member);
            this.tabPage_process.Controls.Add(this.label_project_member);
            this.tabPage_process.Controls.Add(this.textBox_primary_member_list);
            this.tabPage_process.Controls.Add(this.button_output);
            this.tabPage_process.Controls.Add(this.progressBar_fileparsing);
            this.tabPage_process.Controls.Add(this.button_reset);
            this.tabPage_process.Controls.Add(this.button_clear);
            this.tabPage_process.Controls.Add(this.button_parse_file);
            this.tabPage_process.Controls.Add(this.button_load_file);
            this.tabPage_process.Controls.Add(this.button_complete);
            this.tabPage_process.Location = new System.Drawing.Point(4, 22);
            this.tabPage_process.Name = "tabPage_process";
            this.tabPage_process.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_process.Size = new System.Drawing.Size(883, 647);
            this.tabPage_process.TabIndex = 0;
            this.tabPage_process.Text = "Process";
            this.tabPage_process.UseVisualStyleBackColor = true;
            // 
            // button_clear_secondary
            // 
            this.button_clear_secondary.Location = new System.Drawing.Point(516, 100);
            this.button_clear_secondary.Name = "button_clear_secondary";
            this.button_clear_secondary.Size = new System.Drawing.Size(66, 22);
            this.button_clear_secondary.TabIndex = 29;
            this.button_clear_secondary.Text = "Clear";
            this.button_clear_secondary.UseVisualStyleBackColor = true;
            this.button_clear_secondary.Click += new System.EventHandler(this.button_clear_secondary_Click);
            // 
            // button_add_secondary
            // 
            this.button_add_secondary.Location = new System.Drawing.Point(226, 19);
            this.button_add_secondary.Name = "button_add_secondary";
            this.button_add_secondary.Size = new System.Drawing.Size(75, 23);
            this.button_add_secondary.TabIndex = 28;
            this.button_add_secondary.Text = "Secondary";
            this.button_add_secondary.UseVisualStyleBackColor = true;
            this.button_add_secondary.Click += new System.EventHandler(this.button_add_secondary_Click);
            // 
            // button_add_primary
            // 
            this.button_add_primary.Location = new System.Drawing.Point(144, 19);
            this.button_add_primary.Name = "button_add_primary";
            this.button_add_primary.Size = new System.Drawing.Size(75, 23);
            this.button_add_primary.TabIndex = 27;
            this.button_add_primary.Text = "Add Primary";
            this.button_add_primary.UseVisualStyleBackColor = true;
            this.button_add_primary.Click += new System.EventHandler(this.button_add_primary_Click);
            // 
            // label__secondary
            // 
            this.label__secondary.AutoSize = true;
            this.label__secondary.Location = new System.Drawing.Point(3, 86);
            this.label__secondary.Name = "label__secondary";
            this.label__secondary.Size = new System.Drawing.Size(58, 13);
            this.label__secondary.TabIndex = 26;
            this.label__secondary.Text = "Secondary";
            // 
            // label_primary
            // 
            this.label_primary.AutoSize = true;
            this.label_primary.Location = new System.Drawing.Point(6, 44);
            this.label_primary.Name = "label_primary";
            this.label_primary.Size = new System.Drawing.Size(41, 13);
            this.label_primary.TabIndex = 25;
            this.label_primary.Text = "Primary";
            // 
            // textBox_output_path
            // 
            this.textBox_output_path.Location = new System.Drawing.Point(118, 368);
            this.textBox_output_path.Name = "textBox_output_path";
            this.textBox_output_path.Size = new System.Drawing.Size(372, 20);
            this.textBox_output_path.TabIndex = 24;
            // 
            // tabPage_results
            // 
            this.tabPage_results.Controls.Add(this.label_admin);
            this.tabPage_results.Controls.Add(this.label_family_members);
            this.tabPage_results.Controls.Add(this.label_famlies);
            this.tabPage_results.Controls.Add(this.button_save_ancestry_results);
            this.tabPage_results.Controls.Add(this.button_reset_results);
            this.tabPage_results.Controls.Add(this.dataGridView_admin);
            this.tabPage_results.Controls.Add(this.dataGridView_family_members);
            this.tabPage_results.Controls.Add(this.dataGridView_families);
            this.tabPage_results.Location = new System.Drawing.Point(4, 22);
            this.tabPage_results.Name = "tabPage_results";
            this.tabPage_results.Size = new System.Drawing.Size(883, 647);
            this.tabPage_results.TabIndex = 2;
            this.tabPage_results.Text = "Results";
            this.tabPage_results.UseVisualStyleBackColor = true;
            // 
            // listBox_processed_files
            // 
            this.listBox_processed_files.FormattingEnabled = true;
            this.listBox_processed_files.Location = new System.Drawing.Point(0, 186);
            this.listBox_processed_files.Name = "listBox_processed_files";
            this.listBox_processed_files.Size = new System.Drawing.Size(567, 173);
            this.listBox_processed_files.TabIndex = 30;
            // 
            // dataGridView_families
            // 
            this.dataGridView_families.AutoGenerateColumns = false;
            this.dataGridView_families.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_families.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Surname,
            this.Members});
            this.dataGridView_families.DataSource = this.ancestryFamilyBindingSource;
            this.dataGridView_families.Location = new System.Drawing.Point(0, 21);
            this.dataGridView_families.Name = "dataGridView_families";
            this.dataGridView_families.Size = new System.Drawing.Size(244, 524);
            this.dataGridView_families.TabIndex = 1;
            this.dataGridView_families.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_families_ColumnHeaderMouseClick);
            this.dataGridView_families.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_families_RowHeaderMouseClick);
            // 
            // Members
            // 
            this.Members.DataPropertyName = "MemberCount";
            this.Members.HeaderText = "Members";
            this.Members.Name = "Members";
            this.Members.ReadOnly = true;
            // 
            // dataGridView_family_members
            // 
            this.dataGridView_family_members.AutoGenerateColumns = false;
            this.dataGridView_family_members.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_family_members.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.DOB,
            this.POB,
            this.commonDataGridViewCheckBoxColumn,
            this.Admins,
            this.projectcount});
            this.dataGridView_family_members.DataSource = this.ancestryFamilyMemberBindingSource;
            this.dataGridView_family_members.Location = new System.Drawing.Point(250, 21);
            this.dataGridView_family_members.Name = "dataGridView_family_members";
            this.dataGridView_family_members.Size = new System.Drawing.Size(565, 255);
            this.dataGridView_family_members.TabIndex = 2;
            this.dataGridView_family_members.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_family_members_RowHeaderMouseClick);
            // 
            // dataGridView_admin
            // 
            this.dataGridView_admin.AutoGenerateColumns = false;
            this.dataGridView_admin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_admin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.adminIDDataGridViewTextBoxColumn,
            this.iCWOnlyDataGridViewCheckBoxColumn,
            this.Projects});
            this.dataGridView_admin.DataSource = this.ancestryMemberBindingSource;
            this.dataGridView_admin.Location = new System.Drawing.Point(250, 302);
            this.dataGridView_admin.Name = "dataGridView_admin";
            this.dataGridView_admin.Size = new System.Drawing.Size(276, 243);
            this.dataGridView_admin.TabIndex = 3;
            this.dataGridView_admin.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_admin_RowHeaderMouseClick);
            // 
            // Projects
            // 
            this.Projects.DataPropertyName = "ProjectCount";
            this.Projects.HeaderText = "Projects";
            this.Projects.Name = "Projects";
            this.Projects.ReadOnly = true;
            this.Projects.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Projects.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Projects.Width = 50;
            // 
            // DOB
            // 
            this.DOB.DataPropertyName = "BirthYear";
            this.DOB.HeaderText = "DOB";
            this.DOB.Name = "DOB";
            this.DOB.ReadOnly = true;
            // 
            // POB
            // 
            this.POB.DataPropertyName = "BirthPlace";
            this.POB.HeaderText = "POB";
            this.POB.Name = "POB";
            this.POB.ReadOnly = true;
            // 
            // Admins
            // 
            this.Admins.DataPropertyName = "AdminCount";
            this.Admins.HeaderText = "Admins";
            this.Admins.Name = "Admins";
            this.Admins.ReadOnly = true;
            this.Admins.Width = 60;
            // 
            // projectcount
            // 
            this.projectcount.DataPropertyName = "ProjectMemberCount";
            this.projectcount.HeaderText = "Projects";
            this.projectcount.Name = "projectcount";
            this.projectcount.ReadOnly = true;
            this.projectcount.Width = 60;
            // 
            // button_reset_results
            // 
            this.button_reset_results.Location = new System.Drawing.Point(821, 3);
            this.button_reset_results.Name = "button_reset_results";
            this.button_reset_results.Size = new System.Drawing.Size(59, 23);
            this.button_reset_results.TabIndex = 4;
            this.button_reset_results.Text = "Reset";
            this.button_reset_results.UseVisualStyleBackColor = true;
            this.button_reset_results.Click += new System.EventHandler(this.button_reset_results_Click);
            // 
            // button_save_ancestry_results
            // 
            this.button_save_ancestry_results.Location = new System.Drawing.Point(821, 32);
            this.button_save_ancestry_results.Name = "button_save_ancestry_results";
            this.button_save_ancestry_results.Size = new System.Drawing.Size(59, 23);
            this.button_save_ancestry_results.TabIndex = 5;
            this.button_save_ancestry_results.Text = "Save";
            this.button_save_ancestry_results.UseVisualStyleBackColor = true;
            this.button_save_ancestry_results.Click += new System.EventHandler(this.button_save_ancestry_results_Click);
            // 
            // label_projects
            // 
            this.label_projects.AutoSize = true;
            this.label_projects.Location = new System.Drawing.Point(3, 3);
            this.label_projects.Name = "label_projects";
            this.label_projects.Size = new System.Drawing.Size(45, 13);
            this.label_projects.TabIndex = 29;
            this.label_projects.Text = "Projects";
            // 
            // label_members
            // 
            this.label_members.AutoSize = true;
            this.label_members.Location = new System.Drawing.Point(291, 17);
            this.label_members.Name = "label_members";
            this.label_members.Size = new System.Drawing.Size(50, 13);
            this.label_members.TabIndex = 30;
            this.label_members.Text = "Members";
            // 
            // label_files
            // 
            this.label_files.AutoSize = true;
            this.label_files.Location = new System.Drawing.Point(3, 100);
            this.label_files.Name = "label_files";
            this.label_files.Size = new System.Drawing.Size(28, 13);
            this.label_files.TabIndex = 31;
            this.label_files.Text = "Files";
            // 
            // label_famlies
            // 
            this.label_famlies.AutoSize = true;
            this.label_famlies.Location = new System.Drawing.Point(6, 5);
            this.label_famlies.Name = "label_famlies";
            this.label_famlies.Size = new System.Drawing.Size(44, 13);
            this.label_famlies.TabIndex = 6;
            this.label_famlies.Text = "Families";
            // 
            // label_family_members
            // 
            this.label_family_members.AutoSize = true;
            this.label_family_members.Location = new System.Drawing.Point(250, 3);
            this.label_family_members.Name = "label_family_members";
            this.label_family_members.Size = new System.Drawing.Size(50, 13);
            this.label_family_members.TabIndex = 7;
            this.label_family_members.Text = "Members";
            // 
            // label_admin
            // 
            this.label_admin.AutoSize = true;
            this.label_admin.Location = new System.Drawing.Point(253, 283);
            this.label_admin.Name = "label_admin";
            this.label_admin.Size = new System.Drawing.Size(36, 13);
            this.label_admin.TabIndex = 8;
            this.label_admin.Text = "Admin";
            // 
            // adminIDDataGridViewTextBoxColumn
            // 
            this.adminIDDataGridViewTextBoxColumn.DataPropertyName = "AdminID";
            this.adminIDDataGridViewTextBoxColumn.HeaderText = "AdminID";
            this.adminIDDataGridViewTextBoxColumn.Name = "adminIDDataGridViewTextBoxColumn";
            this.adminIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.adminIDDataGridViewTextBoxColumn.Width = 110;
            // 
            // iCWOnlyDataGridViewCheckBoxColumn
            // 
            this.iCWOnlyDataGridViewCheckBoxColumn.DataPropertyName = "ICWOnly";
            this.iCWOnlyDataGridViewCheckBoxColumn.HeaderText = "ICWOnly";
            this.iCWOnlyDataGridViewCheckBoxColumn.Name = "iCWOnlyDataGridViewCheckBoxColumn";
            this.iCWOnlyDataGridViewCheckBoxColumn.ReadOnly = true;
            this.iCWOnlyDataGridViewCheckBoxColumn.Width = 60;
            // 
            // ancestryMemberBindingSource
            // 
            this.ancestryMemberBindingSource.DataSource = typeof(DNAFilesProcessor.Ancestry.AncestryMember);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commonDataGridViewCheckBoxColumn
            // 
            this.commonDataGridViewCheckBoxColumn.DataPropertyName = "Common";
            this.commonDataGridViewCheckBoxColumn.HeaderText = "Common";
            this.commonDataGridViewCheckBoxColumn.Name = "commonDataGridViewCheckBoxColumn";
            this.commonDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // ancestryFamilyMemberBindingSource
            // 
            this.ancestryFamilyMemberBindingSource.DataSource = typeof(DNAFilesProcessor.Ancestry.AncestryFamilyMember);
            // 
            // Surname
            // 
            this.Surname.DataPropertyName = "Surname";
            this.Surname.HeaderText = "Surname";
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            // 
            // ancestryFamilyBindingSource
            // 
            this.ancestryFamilyBindingSource.DataSource = typeof(DNAFilesProcessor.Ancestry.AncestryFamily);
            // 
            // ancestryFamilyBindingSource1
            // 
            this.ancestryFamilyBindingSource1.DataSource = typeof(DNAFilesProcessor.Ancestry.AncestryFamily);
            // 
            // DNAParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 577);
            this.Controls.Add(this.tabControl_Process);
            this.Name = "DNAParser";
            this.Text = "DNA File Processing";
            this.tabControl_Process.ResumeLayout(false);
            this.tabPage_members.ResumeLayout(false);
            this.tabPage_members.PerformLayout();
            this.tabPage_process.ResumeLayout(false);
            this.tabPage_process.PerformLayout();
            this.tabPage_results.ResumeLayout(false);
            this.tabPage_results.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_families)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_family_members)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_admin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryMemberBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryFamilyMemberBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryFamilyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ancestryFamilyBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_load_file;
        private System.Windows.Forms.Button button_parse_file;
        private System.Windows.Forms.Label label_project_member;
        private System.Windows.Forms.ComboBox comboBox_process_member;
        private System.Windows.Forms.ComboBox comboBox_filetype;
        private System.Windows.Forms.ProgressBar progressBar_fileparsing;
        private System.Windows.Forms.Button button_complete;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.TextBox textBox_primary_member_list;
        private System.Windows.Forms.Button button_output;
        private System.Windows.Forms.TextBox textBox_secondary_member_list;
        private System.Windows.Forms.TabControl tabControl_Process;
        private System.Windows.Forms.TabPage tabPage_process;
        private System.Windows.Forms.TabPage tabPage_members;
        private System.Windows.Forms.Button button_add_project;
        private System.Windows.Forms.ComboBox comboBox_surname_projects;
        private System.Windows.Forms.Button button_add_member;
        private System.Windows.Forms.ComboBox comboBox_project_members;
        private System.Windows.Forms.TextBox textBox_member_name;
        private System.Windows.Forms.TextBox textBox_output_path;
        private System.Windows.Forms.Button button_update_member;
        private System.Windows.Forms.Button button_add_file;
        private System.Windows.Forms.TextBox textBox_member_notes;
        private System.Windows.Forms.Label label_member_notes;
        private System.Windows.Forms.Label label_member_name;
        private System.Windows.Forms.Button button_clear_files;
        private System.Windows.Forms.Button button_remove_member;
        private System.Windows.Forms.Button button_remove_project;
        private System.Windows.Forms.Button button_save_project;
        private System.Windows.Forms.ListView listView_member_files;
        private System.Windows.Forms.ColumnHeader fileTypeHeader;
        private System.Windows.Forms.ColumnHeader fileNameHeader;
        private System.Windows.Forms.Label label__secondary;
        private System.Windows.Forms.Label label_primary;
        private System.Windows.Forms.Button button_add_primary;
        private System.Windows.Forms.Button button_load_projects;
        private System.Windows.Forms.Button button_add_secondary;
        private System.Windows.Forms.Button button_clear_secondary;
        private System.Windows.Forms.TabPage tabPage_results;
        private System.Windows.Forms.ListBox listBox_processed_files;
        private System.Windows.Forms.DataGridView dataGridView_families;
        private System.Windows.Forms.BindingSource ancestryFamilyBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Members;
        private System.Windows.Forms.DataGridView dataGridView_admin;
        private System.Windows.Forms.DataGridView dataGridView_family_members;
        private System.Windows.Forms.BindingSource ancestryMemberBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn adminIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iCWOnlyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Projects;
        private System.Windows.Forms.BindingSource ancestryFamilyMemberBindingSource;
        private System.Windows.Forms.BindingSource ancestryFamilyBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn POB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn commonDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Admins;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectcount;
        private System.Windows.Forms.Button button_reset_results;
        private System.Windows.Forms.Button button_save_ancestry_results;
        private System.Windows.Forms.Label label_files;
        private System.Windows.Forms.Label label_members;
        private System.Windows.Forms.Label label_projects;
        private System.Windows.Forms.Label label_admin;
        private System.Windows.Forms.Label label_family_members;
        private System.Windows.Forms.Label label_famlies;
    }
}

