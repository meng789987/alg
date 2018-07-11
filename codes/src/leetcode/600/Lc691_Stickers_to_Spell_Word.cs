using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alg;

/*
 * tags: dp, backtracking
 * Time(2^n), Space(n)
 * dp[s] = 1 + min(dp[t]), t is s removed the letters from a sticker [containing last/any letter of s].
 */
namespace leetcode
{
    public class Lc691_Stickers_to_Spell_Word
    {
        public int MinStickers(string[] stickers, string target)
        {
            var stickerCounts = new int[stickers.Length, 26];
            for (int i = 0; i < stickers.Length; i++)
            {
                foreach (var c in stickers[i])
                    stickerCounts[i, c - 'a']++;
            }

            var memo = new Dictionary<string, int>();
            memo[""] = 0;
            return MinStickersTopdown(stickerCounts, target, memo);
        }

        int MinStickersTopdown(int[,] stickerCounts, string target, Dictionary<string, int> memo)
        {
            if (memo.ContainsKey(target)) return memo[target];

            var targetCount = new int[26];
            foreach (var c in target)
                targetCount[c - 'a']++;

            int min = int.MaxValue;
            for (int i = 0; i < stickerCounts.GetLength(0); i++)
            {
                if (stickerCounts[i, target[0] - 'a'] == 0) continue;
                var sb = new StringBuilder();
                for (int j = 0; j < 26; j++)
                {
                    if (targetCount[j] > stickerCounts[i, j])
                        sb.Append((char)(j + 'a'), targetCount[j] - stickerCounts[i, j]);
                }
                int r = MinStickersTopdown(stickerCounts, sb.ToString(), memo);
                if (0 <= r && r < min) min = r;
            }

            return memo[target] = min == int.MaxValue ? -1 : 1 + min;
        }

        public void Test()
        {
            var stickers = new string[] { "with", "example", "science" };
            Console.WriteLine(MinStickers(stickers, "thehat") == 3);

            stickers = new string[] { "notice", "possible" };
            Console.WriteLine(MinStickers(stickers, "basicbasic") == -1);

            stickers = new string[] { "these", "guess", "about", "garden", "him" };
            Console.WriteLine(MinStickers(stickers, "atomher") == 3);
        }
    }
}

