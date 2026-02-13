namespace SeriouslyGoodSoftware;

public sealed class C4Memory3
{
    /// <summary>
    /// Use arrays to track groups and amounts.
    /// This allows to reduce memory usage by avoiding the need for a group object and using a single array to track group membership.
    /// </summary>
    public static class Container
    {
        // from containerID to its group
        private static int[] group = Array.Empty<int>();

        // from groupID to the amount in each container in the group
        private static float[] amount = Array.Empty<float>();

        public static float GetAmount(int containerId)
        {
            int groupId = group[containerId];
            return amount[groupId];
        }

        public static int NewContainer()
        {
            int nContainers = group.Length;
            int nGroups = amount.Length;

            Array.Resize(ref amount, newSize: nGroups + 1);
            Array.Resize(ref group, newSize: nContainers + 1);

            group[nContainers] = nGroups;
            return nContainers;
        }

        public static void Connect(int containerId1, int containerId2)
        {
            int groupId1 = group[containerId1];
            int groupId2 = group[containerId2];

            if (groupId1 == groupId2) return;

            int size1 = GroupSize(groupId1);
            int size2 = GroupSize(groupId2);

            float amount1 = amount[groupId1] * size1;
            float amount2 = amount[groupId2] * size2;

            amount[groupId1] = (amount1 + amount2) / (size1 + size2);

            for (int i = 0; i < group.Length; i++)
            {
                if (group[i] == groupId2)
                    group[i] = groupId1;
            }

            RemoveGroupAndDefrag(groupId2);
        }

        private static int GroupSize(int groupId)
        {
            int size = 0;
            foreach (int otherGroupId in group)
            {
                if (otherGroupId == groupId)
                    size++;
            }
            return size;
        }

        private static void RemoveGroupAndDefrag(int groupId)
        {
            // Move last group into the deleted group's slot.
            int lastGroupId = amount.Length - 1;

            for (int containerId = 0; containerId < group.Length; containerId++)
            {
                if (group[containerId] == lastGroupId)
                    group[containerId] = groupId;
            }

            amount[groupId] = amount[lastGroupId];

            Array.Resize(ref amount, newSize: amount.Length - 1);
        }

        public static void AddWater(int containerId, float addedAmount)
        {
            int groupId = group[containerId];
            int groupSize = GroupSize(groupId);

            amount[groupId] += addedAmount / groupSize;
        }
    }

    [Fact]
    public void UseCase1()
    {
        int a = Container.NewContainer();
        int b = Container.NewContainer();
        int c = Container.NewContainer();
        int d = Container.NewContainer();
        Assert.Equal(0, actual: Container.GetAmount(a));
        Assert.Equal(0, actual: Container.GetAmount(b));
        Assert.Equal(0, actual: Container.GetAmount(c));
        Assert.Equal(0, actual: Container.GetAmount(d));

        Container.AddWater(a, 12);
        Container.AddWater(d, 8);
        Assert.Equal(12, actual: Container.GetAmount(a));
        Assert.Equal(0, actual: Container.GetAmount(b));
        Assert.Equal(0, actual: Container.GetAmount(c));
        Assert.Equal(8, actual: Container.GetAmount(d));

        Container.Connect(a, b);
        Assert.Equal(6, actual: Container.GetAmount(a));
        Assert.Equal(6, actual: Container.GetAmount(b));
        Assert.Equal(0, actual: Container.GetAmount(c));
        Assert.Equal(8, actual: Container.GetAmount(d));

        Container.Connect(b, c);
        Assert.Equal(4, actual: Container.GetAmount(a));
        Assert.Equal(4, actual: Container.GetAmount(b));
        Assert.Equal(4, actual: Container.GetAmount(c));
        Assert.Equal(8, actual: Container.GetAmount(d));

        Container.Connect(c, d);
        Assert.Equal(5, actual: Container.GetAmount(a));
        Assert.Equal(5, actual: Container.GetAmount(b));
        Assert.Equal(5, actual: Container.GetAmount(c));
        Assert.Equal(5, actual: Container.GetAmount(d));
    }
}
