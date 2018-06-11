namespace leetcode
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

        public ListNode(int[] nums)
        {
            val = nums[0];
            var p = this;
            for (int i = 1; i < nums.Length; i++)
                p = p.next = new ListNode(nums[i]);
        }
    }
}

