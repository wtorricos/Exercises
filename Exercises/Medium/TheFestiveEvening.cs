﻿using Xunit;

namespace Exercises.Medium
{
    /*
        It's the end of July – the time when a festive evening is held at Jelly Castle!
        Guests from all over the kingdom gather here to discuss new trends in the world
        of confectionery. Yet some of the things discussed here are not supposed to be
        disclosed to the general public: the information can cause discord in the kingdom
        of Sweetland in case it turns out to reach the wrong hands. So it's a necessity
        to not let any uninvited guests in.

        There are 26 entrances in Jelly Castle, enumerated with uppercase English letters
        from A to Z. Because of security measures, each guest is known to be assigned an
        entrance he should enter the castle through. The door of each entrance is opened
        right before the first guest's arrival and closed right after the arrival of the
        last guest that should enter the castle through this entrance.
        No two guests can enter the castle simultaneously.

        For an entrance to be protected from possible intrusion, a candy guard should be
        assigned to it. There are k such guards in the castle, so if there are more than k
        opened doors, one of them is going to be left unguarded! Notice that a guard can't
        leave his post until the door he is assigned to is closed.

        Slastyona had a suspicion that there could be uninvited guests at the evening.
        She knows the order in which the invited guests entered the castle, and wants you to
        help her check whether there was a moment when more than k doors were opened.

        Input
        Two integers are given in the first string: the number of guests n and the number of
        guards k (1 ≤ n ≤ 106, 1 ≤ k ≤ 26).

        In the second string, n uppercase English letters s1s2... sn are given, where si
        is the entrance used by the i-th guest.

        Output
        Output «YES» if at least one door was unguarded during some time, and «NO» otherwise.

        You can output each letter in arbitrary case (upper or lower).
     */
    public class TheFestiveEvening
    {
        [Theory]
        [InlineData("5 1", "AABBB", "NO")]
        [InlineData("5 1", "ABABB", "YES")]
        [InlineData("26 1", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "NO")]
        [InlineData("27 1", "ABCDEFGHIJKLMNOPQRSTUVWXYZA", "YES")]
        public void Scenarios(string input1, string input2, string expected)
        {
            var actual = TheFestiveEveningSolution.Solve(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}
