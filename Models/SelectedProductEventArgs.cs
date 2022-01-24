namespace RestClient.Models
{
    public class SelectedProductEventArgs : EventArgs
    {
        public int[] SelectedItemIds { get; init; }
        public SelectedProductEventArgs(int[] selectedItemIds)
        {
            SelectedItemIds = selectedItemIds;
        }
    }
}
