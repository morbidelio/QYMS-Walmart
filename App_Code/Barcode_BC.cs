using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KeepAutomation.Barcode.RDLC;

public class Barcode_BC
{

    private string m_barcodeName;
    private int m_barcodeId;
    private byte[] m_byte;
    public Barcode_BC(string barcodeName, int barcodeId, string data)
    {
        m_barcodeName = barcodeName;
        m_barcodeId = barcodeId;
        BarCode barcode = new BarCode();
        barcode.Symbology = KeepAutomation.Barcode.Symbology.Code128B;
        barcode.CodeToEncode = data;
        m_byte = barcode.generateBarcodeToByteArray();
    }
    public byte[] Byte
    {
        get { return m_byte; }
    }
    public string BarcodeName
    {
        get { return m_barcodeName; }
    }
    public int BarcodeId
    {
        get { return m_barcodeId; }
    }
}
public class Merchant
{
    private List<Barcode_BC> m_barcodes;
    public Merchant()
    {
        m_barcodes = new List<Barcode_BC>();
        m_barcodes.Add(new Barcode_BC("code11", 001, "code11"));
        m_barcodes.Add(new Barcode_BC("code25", 002, "code25"));
        m_barcodes.Add(new Barcode_BC("code39", 003, "code39"));
        m_barcodes.Add(new Barcode_BC("code93", 004, "code93"));
        m_barcodes.Add(new Barcode_BC("code128", 005, "code128"));
        m_barcodes.Add(new Barcode_BC("upcean", 006, "upcean"));
    }
    public List<Barcode_BC> GetProducts()
    {
        return m_barcodes;
    }
}