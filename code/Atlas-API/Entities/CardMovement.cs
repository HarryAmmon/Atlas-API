namespace Atlas_API.Entities
{
    public class CardMovement
    {
        public string DroppableId { get; set; }
        public int Index { get; set; }

        public override string ToString()
        {
            return $"ColumnId: {DroppableId} Index: {Index}";
        }
    }
}