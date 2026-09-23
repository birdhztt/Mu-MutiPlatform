using System.Text;

namespace UnityMU.AssetTool;

public sealed class MainForm : Form
{
    private readonly TextBox rootBox = new() { Dock = DockStyle.Fill };
    private readonly Button browseButton = new() { Text = "选择目录...", AutoSize = true };
    private readonly Button scanButton = new() { Text = "扫描资源", AutoSize = true };
    private readonly Button jsonButton = new() { Text = "导出 JSON", AutoSize = true, Enabled = false };
    private readonly Button csvButton = new() { Text = "导出 CSV", AutoSize = true, Enabled = false };
    private readonly ProgressBar progress = new() { Dock = DockStyle.Fill };
    private readonly Label status = new() { Text = "请选择 Source Main 5.2/bin/Data 或其上级目录", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
    private readonly TextBox details = new() { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, Font = new Font("Consolas", 9) };
    private IReadOnlyList<AssetRecord> assets = Array.Empty<AssetRecord>();

    public MainForm()
    {
        Text = "UnityMU Asset Tool - BMD/OZJ/OZT 资源扫描器";
        Width = 1280;
        Height = 760;
        MinimumSize = new Size(900, 600);
        browseButton.Click += (_, _) => Browse();
        scanButton.Click += async (_, _) => await ScanAsync();
        jsonButton.Click += (_, _) => ExportJson();
        csvButton.Click += (_, _) => ExportCsv();
        grid.SelectionChanged += (_, _) => ShowSelected();
        BuildLayout();
    }

    private void BuildLayout()
    {
        var rootPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(8) };
        rootPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        rootPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        rootPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        rootPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));

        var pathPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 5 };
        pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
        pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        pathPanel.Controls.Add(new Label { Text = "资源目录", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        pathPanel.Controls.Add(rootBox, 1, 0);
        pathPanel.Controls.Add(browseButton, 2, 0);
        pathPanel.Controls.Add(scanButton, 3, 0);
        pathPanel.Controls.Add(jsonButton, 4, 0);

        var statusPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3 };
        statusPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        statusPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        statusPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        statusPanel.Controls.Add(status, 0, 0);
        statusPanel.Controls.Add(progress, 1, 0);
        statusPanel.Controls.Add(csvButton, 2, 0);

        rootPanel.Controls.Add(pathPanel, 0, 0);
        rootPanel.Controls.Add(statusPanel, 0, 1);
        rootPanel.Controls.Add(grid, 0, 2);
        rootPanel.Controls.Add(details, 0, 3);
        Controls.Add(rootPanel);
    }

    private void Browse()
    {
        using var dialog = new FolderBrowserDialog { Description = "选择客户端 Data 或 Player 资源目录" };
        if (dialog.ShowDialog(this) == DialogResult.OK) rootBox.Text = dialog.SelectedPath;
    }

    private async Task ScanAsync()
    {
        if (!Directory.Exists(rootBox.Text)) { MessageBox.Show(this, "目录不存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        Toggle(false);
        progress.Value = 0;
        status.Text = "正在扫描...";
        try
        {
            var scanner = new AssetScanner();
            var result = await Task.Run(() => scanner.Scan(rootBox.Text, new Progress<int>(value => progress.Value = value)));
            assets = result;
            grid.DataSource = assets.Select(x => new { x.RelativePath, x.Category, x.Extension, x.IsPlayerResource, x.Size, x.Sha256 }).ToList();
            status.Text = $"扫描完成：{assets.Count} 个文件；模型 {assets.Count(x => x.Category == "Model")}；纹理 {assets.Count(x => x.Category == "Texture")}；Player 资源 {assets.Count(x => x.IsPlayerResource)}";
            jsonButton.Enabled = csvButton.Enabled = true;
        }
        catch (Exception ex) { status.Text = "扫描失败"; MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        finally { Toggle(true); }
    }

    private void ExportJson()
    {
        using var dialog = new SaveFileDialog { Filter = "JSON 文件|*.json", FileName = "muassets.json" };
        if (dialog.ShowDialog(this) == DialogResult.OK) { AssetScanner.WriteJson(dialog.FileName, rootBox.Text, assets); status.Text = $"已导出 {dialog.FileName}"; }
    }

    private void ExportCsv()
    {
        using var dialog = new SaveFileDialog { Filter = "CSV 文件|*.csv", FileName = "muassets.csv" };
        if (dialog.ShowDialog(this) == DialogResult.OK) { AssetScanner.WriteCsv(dialog.FileName, assets); status.Text = $"已导出 {dialog.FileName}"; }
    }

    private void ShowSelected()
    {
        if (grid.CurrentRow?.Index is not int index || index < 0 || index >= assets.Count) return;
        var asset = assets[index];
        details.Text = $"文件: {asset.RelativePath}\r\n类型: {asset.Category}\r\n大小: {asset.Size:N0} bytes\r\nSHA-256: {asset.Sha256}\r\n\r\n诊断:\r\n{string.Join("\r\n", asset.Diagnostics)}";
    }

    private void Toggle(bool enabled)
    {
        browseButton.Enabled = scanButton.Enabled = enabled;
        jsonButton.Enabled = csvButton.Enabled = enabled && assets.Count > 0;
    }
}
