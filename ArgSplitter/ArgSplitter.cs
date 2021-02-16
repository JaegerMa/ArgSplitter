using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgSplitter
{
	public static class ArgSplitter
	{
		public static IEnumerable<string> SplitArgs(this string input, int maxParts = int.MaxValue, bool removeAllEscapeSequences = false)
		{
			var chars = input.Trim().ToCharArray().ToList();
			var fragments = new List<string>();

			int parts = 0;
			int nextFragmentStart = 0;
			bool inBounds = false;

			for(int i = 0; i < chars.Count; ++i)
			{
				var c = chars[i];
				if(c == '\\')
				{
					if(!removeAllEscapeSequences && (i + 1 >= chars.Count || !isEscapeable(chars[i + 1])))
						continue;

					chars.RemoveAt(i);
					continue;
				}

				//Interpret " only as start or end of a single argument,
				//if it's not in the middle of a string
				if(c == '"' && (!inBounds
								? i == nextFragmentStart
								: (i + 1 == chars.Count || isSpace(chars[i + 1]))))
				{
					inBounds = !inBounds;
					chars.RemoveAt(i);
					--i;
					continue;
				}

				if(inBounds)
					continue;

				if(isSpace(c))
				{
					AddFragment(nextFragmentStart, i);
					nextFragmentStart = i + 1;

					//Create maxParts - 1 parts as the last part isn't added here
					if(++parts + 1 >= maxParts)
						break;
				}
			}

			if(nextFragmentStart < chars.Count)
				AddFragment(nextFragmentStart);

			return fragments;


			bool isSpace(char c)
			{
				return c == ' ' || c == '\t';
			}
			bool isEscapeable(char c)
			{
				switch(c)
				{
					case ' ':
					case '"':
					case '\\':
						return true;
					default:
						return false;
				}
			}
			void AddFragment(int start, int end = -1)
			{
				if(end <= start && end >= 0)
					return;

				if(end < 0)
					end = chars.Count;

				var fragment = string.Join("", chars.GetRange(start, end - start));
				fragments.Add(fragment);
			}
		}
	}
}
