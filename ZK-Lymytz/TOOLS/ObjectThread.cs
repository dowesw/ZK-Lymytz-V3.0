using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace ZK_Lymytz.TOOLS
{
    public class ObjectThread
    {
        DataGridView _dataGrid;
        ListBox _listView;
        Form _form;
        Button _btn;
        ProgressBar _bar;
        Label _lab;
        GroupBox _grp;
        ToolStripMenuItem _tsmi;
        Thread _thread;
        ComboBox _comboBox;
        CheckBox _checkBox;
        DataGridViewComboBoxCell _dataGridViewComboBoxCell;
        DataGridViewRow _dataGridViewRow;


        #region DataGridView
        public ObjectThread(DataGridView _data_)
        {
            this._dataGrid = _data_;
        }

        public delegate void delegateClearDataGridView(bool i);
        public void ClearDataGridView(bool i)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    try
                    {
                        delegateClearDataGridView deleg = new delegateClearDataGridView(ClearDataGridView);
                        _dataGrid.Invoke(deleg, new object[] { i });
                    }
                    catch (Exception ex)
                    {
                        Messages.Exception("ObjectThread (ClearDataGridView) ", ex);
                    }
                }
                else
                {
                    _dataGrid.Rows.Clear();
                }
            }
        }

        public delegate void delegateRemoveDataGridView(int i);
        public void RemoveDataGridView(int i)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    delegateRemoveDataGridView deleg = new delegateRemoveDataGridView(RemoveDataGridView);
                    _dataGrid.Invoke(deleg, new object[] { i });
                }
                else
                {
                    if (i > -1)
                        _dataGrid.Rows.RemoveAt(i);
                }
            }
        }

        public delegate void delegateInsertDataGridView(object[] data);
        public void WriteDataGridView(object[] data)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    delegateInsertDataGridView deleg = new delegateInsertDataGridView(WriteDataGridView);
                    _dataGrid.Invoke(deleg, new object[] { data });
                }
                else
                {
                    _dataGrid.Rows.Add(data);
                }
            }
        }

        public delegate void delegateUpdateDataGridView(int i, object[] data);
        public void WriteDataGridView(int i, object[] data)
        {
            try
            {
                if (_dataGrid != null)
                {
                    if (_dataGrid.InvokeRequired)
                    {
                        delegateUpdateDataGridView deleg = new delegateUpdateDataGridView(WriteDataGridView);
                        _dataGrid.Invoke(deleg, new object[] { i, data });
                    }
                    else
                    {
                        _dataGrid.Rows.Insert(i, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }
        #endregion

        #region ListBox
        public ObjectThread(ListBox _data_)
        {
            this._listView = _data_;
        }

        public delegate void delegateWriteListBox(string text);
        public void WriteListBox(string text)
        {
            if (_listView != null)
            {
                if (_listView.InvokeRequired)
                {
                    delegateWriteListBox deleg = new delegateWriteListBox(WriteListBox);
                    _listView.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _listView.Items.Add(text);
                }
            }
        }

        public delegate void delegateUpdateListBox(int index, string text);
        public void UpdateListBox(int index, string text)
        {
            if (_listView != null)
            {
                if (_listView.InvokeRequired)
                {
                    delegateUpdateListBox deleg = new delegateUpdateListBox(UpdateListBox);
                    _listView.Invoke(deleg, new object[] { index, text });
                }
                else
                {
                    _listView.Items.Insert(index, text);
                }
            }
        }

        public delegate void delegateRemoveListBox(int index);
        public void RemoveListBox(int index)
        {
            if (_listView != null)
            {
                if (_listView.InvokeRequired)
                {
                    delegateRemoveListBox deleg = new delegateRemoveListBox(RemoveListBox);
                    _listView.Invoke(deleg, new object[] { index });
                }
                else
                {
                    _listView.Items.RemoveAt(index);
                }
            }
        }

        public delegate void delegateClearListBox(bool clear);
        public void ClearListBox(bool clear)
        {
            if (_listView != null)
            {
                if (_listView.InvokeRequired)
                {
                    delegateClearListBox deleg = new delegateClearListBox(ClearListBox);
                    _listView.Invoke(deleg, new object[] { clear });
                }
                else
                {
                    _listView.Items.Clear();
                }
            }
        }
        #endregion

        #region Form
        public ObjectThread(Form _data_)
        {
            this._form = _data_;
        }
        public delegate void delegateUpdateTextForm(string text);
        public void WriteTextForm(string text)
        {
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    delegateUpdateTextForm deleg = new delegateUpdateTextForm(WriteTextForm);
                    _form.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _form.Text = text;
                }
            }
        }

        public void Show()
        {
            if (_form != null)
            {
                Constantes.RUN_SECOND = new Thread(() =>
                {
                    var form = Utils.GetForm(_form.GetType().ToString());
                    form.Show();
                    Application.Run();
                });
                Constantes.RUN_SECOND.SetApartmentState(ApartmentState.STA);
                Constantes.RUN_SECOND.Start();
            }
        }

        public void Close()
        {
            if (Constantes.RUN_SECOND != null)
            {
                //Constantes.RUN_SECOND.Abort();
                Application.Exit();
            }
        }

        public delegate void delegateOpenForm();
        public void OpenForm()
        {
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    delegateOpenForm deleg = new delegateOpenForm(OpenForm);
                    _form.Invoke(deleg, new object[] { });
                }
                else
                {
                    _form.Show();
                }
            }
        }

        public delegate void delegateDisposeForm(bool dispose);
        public void DisposeForm(bool dispose)
        {
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    delegateDisposeForm deleg = new delegateDisposeForm(DisposeForm);
                    _form.Invoke(deleg, new object[] { dispose });
                }
                else
                {
                    _form.Close();
                    if (dispose)
                        _form.Dispose();
                }
            }
        }

        public delegate void delegateVisibleForm(bool visible);
        public void VisibleForm(bool visible)
        {
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    delegateVisibleForm deleg = new delegateVisibleForm(DisposeForm);
                    _form.Invoke(deleg, new object[] { visible });
                }
                else
                {
                    _form.Visible = visible;
                }
            }
        }

        #endregion

        #region Button
        public ObjectThread(Button _btn_)
        {
            this._btn = _btn_;
        }

        public delegate void delegateEnableButton(bool enable);
        public void EnableButton(bool enable)
        {
            if (_btn != null)
            {
                if (_btn.InvokeRequired)
                {
                    delegateEnableButton deleg = new delegateEnableButton(EnableButton);
                    _btn.Invoke(deleg, new object[] { enable });
                }
                else
                {
                    _btn.Enabled = enable;
                }
            }
        }

        public delegate void delegateTextButton(string text);
        public void TextButton(string text)
        {
            if (_btn != null)
            {
                if (_btn.InvokeRequired)
                {
                    delegateTextButton deleg = new delegateTextButton(TextButton);
                    _btn.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _btn.Text = text;
                }
            }
        }

        #endregion

        #region ProgressBar
        public ObjectThread(ProgressBar _bar_)
        {
            this._bar = _bar_;
        }
        public delegate void delegateUpdateBar(int value);
        public void UpdateBar(int value)
        {
            _UpdateBar(value, "Veuillez Patientez");
        }

        public delegate void _delegateUpdateBar(int value, string msg);
        public void _UpdateBar(int value, string msg)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    try
                    {
                        _delegateUpdateBar deleg = new _delegateUpdateBar(_UpdateBar);
                        _bar.Invoke(deleg, new object[] { value, msg });
                    }
                    catch (Exception ex)
                    {
                        Messages.Exception("ObjectThread (_UpdateBar) ", ex);
                    }
                }
                else
                {
                    int v = _bar.Value;
                    v += value;
                    string percent_ = msg + "... (";
                    if (v >= _bar.Maximum)
                    {
                        _bar.Value = _bar.Maximum;
                        percent_ = "Opération Terminée... (";
                    }
                    else
                    {
                        _bar.Value = v;
                    }
                    int percent = (int)(((double)(_bar.Value - _bar.Minimum) / (double)(_bar.Maximum - _bar.Minimum)) * 100);
                    percent = percent > 0 ? (percent < 100 ? percent : 100) : 0;
                    percent_ += percent.ToString() + "%)";
                    using (Graphics gr = _bar.CreateGraphics())
                    {
                        float x_ = _bar.Width / 2 - (gr.MeasureString(percent_, SystemFonts.DefaultFont).Width / 2.0F);
                        float y_ = _bar.Height / 2 - (gr.MeasureString(percent_, SystemFonts.DefaultFont).Height / 2.0F);
                        PointF p_ = new PointF(x_, y_);
                        gr.DrawString(percent_, SystemFonts.DefaultFont, Brushes.Black, p_);
                        if (percent >= 100)
                        {
                            Thread.SpinWait(1);
                        }
                    }
                }
            }
        }

        public void UpdateSimpleBar(int value)
        {
            try
            {
                if (_bar != null)
                {
                    if (_bar.InvokeRequired)
                    {
                        delegateUpdateBar deleg = new delegateUpdateBar(UpdateSimpleBar);
                        _bar.Invoke(deleg, new object[] { value });
                    }
                    else
                    {
                        if (_bar.Value == _bar.Maximum)
                            _bar.Value = 0;
                        else
                            _bar.Value += value;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        public void SetValueBar(int value)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    delegateUpdateBar deleg = new delegateUpdateBar(SetValueBar);
                    _bar.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _bar.Value = value;
                }
            }
        }

        public delegate void delegateUpdateMaxBar(int value);
        public void UpdateMaxBar(int value)
        {
            try
            {
                if (_bar != null)
                {
                    if (_bar.InvokeRequired)
                    {
                        delegateUpdateMaxBar deleg = new delegateUpdateMaxBar(UpdateMaxBar);
                        _bar.Invoke(deleg, new object[] { value });
                    }
                    else
                    {
                        _bar.Maximum = value;
                        _bar.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _bar.Maximum = _bar.Maximum;
            }
        }

        public delegate void delegateUpdateColorBar(System.Drawing.Color value);
        public void UpdateColorBar(System.Drawing.Color value)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    delegateUpdateColorBar deleg = new delegateUpdateColorBar(UpdateColorBar);
                    _bar.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _bar.ForeColor = value;
                }
            }
        }

        public delegate void delegateContinuosBar(bool stop);
        public void ContinuosBar(bool stop)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    delegateContinuosBar deleg = new delegateContinuosBar(ContinuosBar);
                    _bar.Invoke(deleg, new object[] { stop });
                }
                else
                {
                    while (!stop)
                    {
                        Thread.Sleep(5);
                        if (_bar.Value == _bar.Maximum)
                        {
                            _bar.Value = 0;
                        }
                        else
                        {
                            _bar.Value++;
                        }
                    }
                }
            }
        }

        #endregion

        #region Label
        public ObjectThread(Label _lab_)
        {
            this._lab = _lab_;
        }

        public delegate void delegateTextLabel(string text);
        public void TextLabel(string text)
        {
            try
            {
                if (_lab != null)
                {
                    if (_lab.InvokeRequired)
                    {
                        delegateTextLabel deleg = new delegateTextLabel(TextLabel);
                        _lab.Invoke(deleg, new object[] { text });
                    }
                    else
                    {
                        _lab.Text = text;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        #endregion

        #region GroupBox
        public ObjectThread(GroupBox _grp_)
        {
            this._grp = _grp_;
        }

        public delegate void delegateTextGroupBox(string text);
        public void TextGroupBox(string text)
        {
            if (_grp != null)
            {
                if (_grp.InvokeRequired)
                {
                    delegateTextLabel deleg = new delegateTextLabel(TextGroupBox);
                    _grp.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _grp.Text = text;
                }
            }
        }

        #endregion

        #region ToolStripMenuItem
        public ObjectThread(ToolStripMenuItem _tsmi_)
        {
            this._tsmi = _tsmi_;
        }

        public delegate void delegateTextToolStrip(ContextMenuStrip context, string text);
        public void TextToolStrip(ContextMenuStrip context, string text)
        {
            if (_tsmi != null)
            {
                if (context.InvokeRequired)
                {
                    delegateTextToolStrip deleg = new delegateTextToolStrip(TextToolStrip);
                    context.Invoke(deleg, new object[] { context, text });
                }
                else
                {
                    _tsmi.Text = text;
                }
            }
        }

        #endregion

        #region Thread
        public ObjectThread(System.Threading.Thread _thread_)
        {
            this._thread = _thread_;
        }


        #endregion

        #region DataGridViewComboBoxColumn
        public ObjectThread(DataGridViewComboBoxCell _dataGridViewComboBoxCell_)
        {
            this._dataGridViewComboBoxCell = _dataGridViewComboBoxCell_;
        }

        public delegate void delegateValueComboBoxCell(string value);
        public void ValueComboBoxCell(string value)
        {
            if (_dataGridViewComboBoxCell != null)
            {
                if (_dataGridViewComboBoxCell.DataGridView.InvokeRequired)
                {
                    delegateValueComboBoxCell deleg = new delegateValueComboBoxCell(ValueComboBoxCell);
                    _dataGridViewComboBoxCell.DataGridView.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _dataGridViewComboBoxCell.Value = value;
                }
            }
        }
        #endregion

        #region DataGridViewRow
        public ObjectThread(DataGridViewRow _dataGridViewRow_)
        {
            this._dataGridViewRow = _dataGridViewRow_;
        }

        public delegate void delegateForeColorDataGridViewRow(Color color);
        public void ForeColorDataGridViewRow(Color color)
        {
            if (_dataGridViewRow != null)
            {
                if (_dataGridViewRow.DataGridView.InvokeRequired)
                {
                    delegateForeColorDataGridViewRow deleg = new delegateForeColorDataGridViewRow(ForeColorDataGridViewRow);
                    _dataGridViewComboBoxCell.DataGridView.Invoke(deleg, new object[] { color });
                }
                else
                {
                    _dataGridViewRow.DefaultCellStyle.ForeColor = color;
                }
            }
        }
        #endregion

        #region ComboBox
        public ObjectThread(ComboBox _comboBox_)
        {
            this._comboBox = _comboBox_;
        }

        public delegate void delegateDisplayMember(string text);
        public void DisplayMember(string text)
        {
            if (_comboBox != null)
            {
                if (_comboBox.InvokeRequired)
                {
                    delegateDisplayMember deleg = new delegateDisplayMember(DisplayMember);
                    _comboBox.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _comboBox.DisplayMember = text;
                }
            }
        }

        public delegate void delegateValueMember(string text);
        public void ValueMember(string text)
        {
            if (_comboBox != null)
            {
                if (_comboBox.InvokeRequired)
                {
                    delegateValueMember deleg = new delegateValueMember(ValueMember);
                    _comboBox.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _comboBox.ValueMember = text;
                }
            }
        }

        public delegate void delegateDataSource(BindingSource dataSource);
        public void DataSource(BindingSource dataSource)
        {
            if (_comboBox != null)
            {
                if (_comboBox.InvokeRequired)
                {
                    delegateDataSource deleg = new delegateDataSource(DataSource);
                    _comboBox.Invoke(deleg, new object[] { dataSource });
                }
                else
                {
                    _comboBox.DataSource = dataSource;
                }
            }
        }

        public delegate void delegateAutoCompleteMode(AutoCompleteMode autoCompleteMode);
        public void AutoCompleteMode(AutoCompleteMode autoCompleteMode)
        {
            if (_comboBox != null)
            {
                if (_comboBox.InvokeRequired)
                {
                    delegateAutoCompleteMode deleg = new delegateAutoCompleteMode(AutoCompleteMode);
                    _comboBox.Invoke(deleg, new object[] { autoCompleteMode });
                }
                else
                {
                    _comboBox.AutoCompleteMode = autoCompleteMode;
                }
            }
        }

        public delegate void delegateAutoCompleteSource(AutoCompleteSource autoCompleteMode);
        public void AutoCompleteSource(AutoCompleteSource autoCompleteSource)
        {
            if (_comboBox != null)
            {
                if (_comboBox.InvokeRequired)
                {
                    delegateAutoCompleteSource deleg = new delegateAutoCompleteSource(AutoCompleteSource);
                    _comboBox.Invoke(deleg, new object[] { autoCompleteSource });
                }
                else
                {
                    _comboBox.AutoCompleteSource = autoCompleteSource;
                }
            }
        }

        public delegate void delegateAutoCompleteCustomSource_Add(string value);
        public void AutoCompleteCustomSource_Add(string value)
        {
            if (_comboBox != null)
            {
                if (_comboBox.InvokeRequired)
                {
                    delegateAutoCompleteCustomSource_Add deleg = new delegateAutoCompleteCustomSource_Add(AutoCompleteCustomSource_Add);
                    _comboBox.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _comboBox.AutoCompleteCustomSource.Add(value);
                }
            }
        }

        #endregion

        #region CheckBox
        public ObjectThread(CheckBox _checkBox_)
        {
            this._checkBox = _checkBox_;
        }

        public delegate void delegateChecked(bool checke);
        public void Checked(bool checke)
        {
            if (_checkBox != null)
            {
                if (_checkBox.InvokeRequired)
                {
                    delegateChecked deleg = new delegateChecked(Checked);
                    _checkBox.Invoke(deleg, new object[] { checke });
                }
                else
                {
                    _checkBox.Checked = checke;
                }
            }
        }

        #endregion

    }
}
