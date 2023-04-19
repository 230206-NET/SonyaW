public class CodingChallenges {
    public static void Main(string[] args) {
        CodingChallenges app = new CodingChallenges();
        app.capLettersIndex("HeLl0 WoRld!AZ");
    }

    public List<int> capLettersIndex(string s) {
        List<int> indexes = new List<int>();
        for(int i = 0; i < s.Length; i++) {
            if((int)((char)s[i]) >= 65 && (int)((char)s[i]) < 91) {
                indexes.Add(i);
                Console.WriteLine("{0}: {1}", i, (int)((char)s[i]));
            }
        }
        return indexes;
    }
}