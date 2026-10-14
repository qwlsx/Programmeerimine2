using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms;

public class MainForm : Form, IMainView
{
    private readonly MainViewPresenter _presenter;
    private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, AutoGenerateColumns = true, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
    private readonly TextBox _name = new() { Dock = DockStyle.Fill };
    private readonly DateTimePicker _start = new() { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy HH:mm" };
    private readonly TextBox _description = new() { Dock = DockStyle.Fill, Multiline = true, Height = 80 };
    private readonly NumericUpDown _seats = new() { Dock = DockStyle.Fill, Minimum = 1, Maximum = 100000 };
    private readonly NumericUpDown _price = new() { Dock = DockStyle.Fill, DecimalPlaces = 2, Maximum = 1000000 };
    private int _currentId;

    public MainForm(IApiClient apiClient)
    {
        Text = "Мероприятия";
        Width = 1100;
        Height = 650;
        StartPosition = FormStartPosition.CenterScreen;
        _presenter = new MainViewPresenter(this, apiClient);
        BuildLayout();
        Load += async (_, _) => await _presenter.LoadData();
        _grid.SelectionChanged += (_, _) => _presenter.SetSelection(_grid.CurrentRow?.DataBoundItem as EventModel);
    }

    public IList<EventModel> DataSource
    {
        set => _grid.DataSource = value.ToList();
    }
    public int CurrentId { get => _currentId; set => _currentId = value; }
    public string CurrentName { get => _name.Text; set => _name.Text = value; }
    public DateTime CurrentStartDateTime { get => _start.Value; set => _start.Value = value < _start.MinDate ? _start.MinDate : value; }
    public string? CurrentDescription { get => _description.Text; set => _description.Text = value ?? string.Empty; }
    public int CurrentTotalSeats { get => (int)_seats.Value; set => _seats.Value = Math.Clamp(value, (int)_seats.Minimum, (int)_seats.Maximum); }
    public decimal CurrentPrice { get => _price.Value; set => _price.Value = Math.Clamp(value, _price.Minimum, _price.Maximum); }

    public void ShowError(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    public bool ConfirmDelete() => MessageBox.Show("Удалить выбранное мероприятие?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    private void BuildLayout()
    {
        var split = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 650 };
        split.Panel1.Controls.Add(_grid);
        var form = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(16), AutoScroll = true };
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        AddField(form, "Название", _name);
        AddField(form, "Начало", _start);
        AddField(form, "Описание", _description);
        AddField(form, "Количество мест", _seats);
        AddField(form, "Цена", _price);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        var add = new Button { Text = "Добавить", AutoSize = true };
        var save = new Button { Text = "Сохранить", AutoSize = true };
        var delete = new Button { Text = "Удалить", AutoSize = true };
        add.Click += (_, _) => _presenter.AddNew();
        save.Click += async (_, _) => await _presenter.Save();
        delete.Click += async (_, _) => await _presenter.Delete();
        buttons.Controls.AddRange(new Control[] { add, save, delete });
        form.Controls.Add(buttons, 1, form.RowCount);
        split.Panel2.Controls.Add(form);
        Controls.Add(split);
    }

    private static void AddField(TableLayoutPanel panel, string label, Control control)
    {
        var row = panel.RowCount++;
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 8, 3, 8) }, 0, row);
        control.Margin = new Padding(3, 5, 3, 5);
        panel.Controls.Add(control, 1, row);
    }
}
