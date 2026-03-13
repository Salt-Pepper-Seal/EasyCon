using EasyCon.Capture;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace EasyCon2.Helper;

internal class ILManager : INotifyPropertyChanged
{
    private ImgLabel _cur = new();

    public ImgLabel Current
    {
        get => _cur;
        set
        {
            _cur = new()
            {
                name = value.name,
                searchMethod = value.searchMethod,
                ImgBase64 = value.ImgBase64,
                RangeX = value.RangeX,
                RangeY = value.RangeY,
                RangeHeight = value.RangeHeight,
                RangeWidth = value.RangeWidth,
                TargetX = value.TargetX,
                TargetY = value.TargetY,
                TargetHeight = value.TargetHeight,
                TargetWidth = value.TargetWidth,
            };
            OnPropertyChanged();
        }
    }

    public SearchMethod SearchMethod
    {
        get => _cur.searchMethod;
        set
        {
            _cur.searchMethod = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _cur.name;
        set
        {
            _cur.name = value;
            OnPropertyChanged();
        }
    }

    public int RangeX
    {
        get => _cur.RangeX;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.RangeX)) return;
            _cur.RangeX = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int RangeY
    {
        get => _cur.RangeY;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.RangeY)) return;
            _cur.RangeY = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int RangeWidth
    {
        get => _cur.RangeWidth;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.RangeWidth)) return;
            _cur.RangeWidth = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int RangeHeight
    {
        get => _cur.RangeHeight;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.RangeHeight)) return;
            _cur.RangeHeight = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int TargetX
    {
        get => _cur.TargetX;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.TargetX)) return;
            _cur.TargetX = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int TargetY
    {
        get => _cur.TargetY;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.TargetY)) return;
            _cur.TargetY = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int TargetWidth
    {
        get => _cur.TargetWidth;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.TargetWidth)) return;
            _cur.TargetWidth = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public int TargetHeight
    {
        get => _cur.TargetHeight;
        set
        {
            // do not trigger change event if values are the same
            if (Equals(value, _cur.TargetHeight)) return;
            _cur.TargetHeight = value < 0 ? 0 : value;

            //===================
            // Usage in the Source
            //===================
            OnPropertyChanged();
        }
    }

    public BindingList<ILViewModel> Labels = new BindingList<ILViewModel>();

    public void LoadImgLabels(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"LoadImgLabels: 创建目录失败 {path}: {ex.Message}");
            return;
        }

        Labels.Clear();

        // enumerate common label extensions, case-insensitive and avoid duplicate base names
        var patterns = new[] { "*.il", "*.ilx" };
        var files = new List<string>();
        foreach (var p in patterns)
        {
            try
            {
                files.AddRange(Directory.EnumerateFiles(path, p, SearchOption.TopDirectoryOnly));
            }
            catch { }
        }

        // Group by base filename to avoid duplicates when multiple extensions or case variants exist
        var uniqueFiles = files
            .GroupBy(f => Path.GetFileNameWithoutExtension(f), StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .ToList();

        Debug.WriteLine($"LoadImgLabels: path={path}, found {files.Count} files, unique {uniqueFiles.Count} base names");
        foreach (var file in uniqueFiles)
        {
            try
            {
                var il = ECSearch.LoadIL(file);
                Labels.Add(new ILViewModel(il));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"无法加载标签: {file} ({ex.Message})");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

internal class ILViewModel : INotifyPropertyChanged
{
    private ImgLabel _cur;

    public ILViewModel(ImgLabel label)
    {
        _cur = label;
    }

    public ImgLabel Current => _cur;

    public string Name
    {
        get => _cur.name;
        set
        {
            _cur.name = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
