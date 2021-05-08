using ChapterMaster.UI.Align;

namespace ChapterMaster.UI
{
    public class Ledger : Screen
    {
        public Ledger(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            //Button closeLedger = new Button("creation_arrow_right", "", new CenterAlign());
        }
    }
}