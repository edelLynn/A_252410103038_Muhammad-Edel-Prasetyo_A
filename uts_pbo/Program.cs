using System.Security.Cryptography.X509Certificates;

Console.WriteLine("=== System MBG ===");

class Sekolah
{
    public string idsekolah = "";
    public string namasekolah = "";
    public int jumlahPaketditerima;

    public void TampilkanInfo()
    {
        Console.WriteLine($"ID Sekolah: {idsekolah}");
        Console.WriteLine($"Nama Sekolah: {namasekolah}");
        Console.WriteLine($"Jumlah Paket Diterima: {jumlahPaketditerima}");
    }
}

public abstract class PaketMBG
{
    private int _stokDapur;

    public string idpaket = "";
    public string jenisPaket = "";
    public int stokAwal = 10;

    public int StokDapur
    {
        get => _stokDapur;
        protected set => _stokDapur = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Stok dapur tidak boleh bernilai negatif");
    }

    protected PaketMBG(){}

    protected PaketMBG(string id, string nama, int stokAwal)
    {
        idpaket = id;
        jenisPaket = nama;
        StokDapur = stokAwal;
    }

    public void KurangiStok(int jumlah)
    {
        if (jumlah <= 0)
            throw new ArgumentException("Jumlah pengurangan harus lebih dari 0");
        if (jumlah > _stokDapur)
            throw new InvalidOperationException("Stok tidak mencukupi");

        _stokDapur -= jumlah;
    }

    public abstract int HitungBiaya(int jumlah);

    public virtual void TampilkanInfo()
    {
        Console.WriteLine($"ID Paket: {idpaket}");
        Console.WriteLine($"Nama Paket: {jenisPaket}");
        Console.WriteLine($"Stok Dapur: {StokDapur}");
    }
}

public class PaketRegular : PaketMBG
{
    public PaketRegular() : base() { }
    public PaketRegular(string id, string nama, int stokAwal) : base(id, nama, stokAwal) { }

    public string isiPaket = "Nasi, Ayam, Sayur Dan Buah";

    public override void TampilkanInfo()
    {
        base.TampilkanInfo();
        Console.WriteLine($"Isi Paket: {isiPaket}");
    }

    public override int HitungBiaya(int jumlah)
    {
        return jumlah * 15000;
    }
}

public class PaketKhusus : PaketMBG
{
    public string jenisDiet { get; set; } = "";

    public PaketKhusus() : base() { }
    public PaketKhusus(string id, string nama, int stokAwal, string jenisDiet = "Rendah Gula") : base(id, nama, stokAwal)
    {
        this.jenisDiet = jenisDiet;
    }

    public override void TampilkanInfo()
    {
        base.TampilkanInfo();
        if (!string.IsNullOrEmpty(jenisDiet))
            Console.WriteLine($"Jenis Diet: {jenisDiet}");
    }
    public override int HitungBiaya(int jumlah)
    {
        return jumlah * 20000;
    }
}
partial class Program
{
    static void Main(string[] args)
    {
        Sekolah sekolah1 = new Sekolah();
        sekolah1.idsekolah = "S001";
        sekolah1.namasekolah = "SDN 1 Jakarta";
        sekolah1.jumlahPaketditerima = 5;
        PaketRegular paketRegular = new PaketRegular("P001", "Paket Regular", 10);
        PaketKhusus paketKhusus = new PaketKhusus("P002", "Paket Khusus", 5);
        Console.WriteLine("\nInformasi Sekolah:");
        sekolah1.TampilkanInfo();
        Console.WriteLine("\nInformasi Paket Regular:");
        paketRegular.TampilkanInfo();
        Console.WriteLine("\nInformasi Paket Khusus:");
        paketKhusus.TampilkanInfo();
    }
}