using DinkToPdf;
using DinkToPdf.Contracts;
using System.Collections.Generic;

namespace ClinicalInterview.Api.Templates
{
    public partial class PatientIntakeFormTemplate
    {
        private readonly IConverter converter;

        public Dictionary<string, string> Model { get; set; }

        public PatientIntakeFormTemplate()
        {
            Model = new Dictionary<string, string>();
            converter = new BasicConverter(new PdfTools());
        }

        public string GetValue(string key) =>
            Model.TryGetValue(key, out string value) ? value : string.Empty;

        public byte[] Print()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = TransformText(),
                    }
                }
            };
            return converter.Convert(doc);
        }
    }
}
