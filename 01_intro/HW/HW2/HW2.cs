// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");

            // IMPORTANT: === Giải thích về args và cách tạo đường dẫn ===
            // 1. NOTE: Shell (Terminal/Command Prompt) tách lệnh thành các phần (tokens) dựa trên khoảng trắng.
            // 2. NOTE: Ví dụ: `dotnet run myfile.txt`
            //    Shell sẽ gọi chương trình `dotnet` với tham số `run` và truyền `"myfile.txt"` vào ứng dụng.
            // 3. NOTE: .NET Core runtime khởi tạo mảng `args` từ các token sau tên chương trình:
            //    - "args[0]" = "myfile.txt"
            //    - "args[1], args[2]", … nếu bạn truyền thêm tham số.
            // 4. VAR: `string filePath = args[0];` // filePath chứa giá trị token đầu tiên, là tên hoặc đường dẫn file.
            // 5. NOTE: Nếu `filePath` là đường dẫn tương đối (không có ổ đĩa hoặc không bắt đầu bằng '/'):
            //    .NET sẽ tìm file trong (thư mục làm việc hiện tại) (Current Working Directory):
            //    * 'Khi chạy từ terminal: thư mục bạn đang đứng khi chạy lệnh.'
            //    * 'Khi chạy từ IDE: thư mục gốc của dự án chứa file .csproj.'
            // 6. NOTE: Nếu `filePath` là đường dẫn tuyệt đối (có ký tự ổ đĩa hoặc bắt đầu bằng '/'):
            //    .NET sẽ truy cập trực tiếp đến vị trí đó trên hệ thống tệp.

            // TODO: Kiểm tra xem đã truyền tham số đường dẫn chưa
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }

            // VAR: Lấy tham số đầu tiên làm đường dẫn tệp
            string filePath = args[0];

            // WARNING: Kiểm tra sự tồn tại của tệp
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }

            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");

                // Read the file content
                string content = File.ReadAllText(filePath);

                // TODO: Implement analysis functionality
                // 1. Count words
                // 2. Count characters (with and without whitespace)
                // 3. Count sentences
                // 4. Identify most common words
                // 5. Average word length

                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"1. Number of lines: {lineCount}");

                // Todo: 1. Đếm số từ
                // ----------------------------------------------------------------------------------------------------------
                // IMPORTANT: Sử dụng "Regex.Split" để tách chuỗi `content` thành mảng các chuỗi con dựa trên pattern regex
                // ----------------------------------------------------------------------------------------------------------
                // NOTE: 1) Phương thức: "Regex.Split(input, pattern)"
                //          • Là phương thức tĩnh của lớp 'System.Text.RegularExpressions.Regex'.
                //          • Chức năng: quét toàn bộ chuỗi `input`, tìm mọi vị trí khớp với `pattern` và
                //            dùng những vị trí đó làm điểm ngắt để chia chuỗi thành các phần tử mảng.
                //
                // NOTE: 2) Tham số đầu vào `content` (string)
                //          • Là chuỗi gốc chứa văn bản cần phân tích, có thể bao gồm chữ, số, dấu câu, khoảng trắng, v.v.
                //
                // NOTE: 3) Tham số `@"\W+"` (verbatim string literal chứa regex pattern)
                //      • '@' trước chuỗi: cho phép viết [“\W+”] trực tiếp mà không cần escape thành [“\\W+”].
                //      • '\W' : trong regex, [\w] (ký tự thường) đại diện cho tập [A–Z, a–z, 0–9 và dấu gạch dưới (_),
                //          nên [\W] (ký tự hoa) sẽ khớp với bất kỳ ký tự nào 'không' thuộc tập đó
                //          (ví dụ: dấu cách, dấu câu, ký tự đặc biệt, v.v.).
                //      • '+' : quantifier, nghĩa là (“một hoặc nhiều lần”) — sẽ khớp bất kỳ dãy liên tiếp
                //            gồm '≥1' ký tự không thuộc [A-Za-z0-9_].
                //
                // NOTE: 4) Cách hoạt động chung:
                //        - "Regex.Split" sẽ quét `content` và mỗi khi gặp một (“dãy ký tự phân tách”) khớp \W+,
                //          nó sẽ ngắt chuỗi tại đó.  
                //        - Mỗi đoạn văn bản 'nằm giữa các vị trí ngắt trở thành một phần tử trong mảng' kết quả.  
                //        - Nhờ '+', các ký tự phân tách liên tiếp chỉ tạo ra (một) điểm ngắt duy nhất.
                //        - Nếu chuỗi bắt đầu hoặc kết thúc bằng ký tự phân tách, hoặc có hai phân tách liền nhau,
                //          mảng kết quả có thể chứa phần tử rỗng (`""`).
                //
                // Important: Ví dụ nhanh:
                //    'content' = "Hello, world! 123";
                //    'Regex.Split(content, @"\W+")'
                //      → ["Hello", "world", "123", ""]
                //
                // Kết hợp với 'LINQ' để loại bỏ các phần tử rỗng:
                //    ".Where(w => !string.IsNullOrEmpty(w))"
                //
                // Cuối cùng ta có mảng `words` chỉ chứa các “từ” thực sự:
                // ------------------------------------------------------------------------------------------
                var words = Regex
                    .Split(content, @"\W+")             // tách theo dãy ký tự không phải [ A-Z, a-z, 0-9, _ ]
                    .Where(w => !string.IsNullOrEmpty(w)) // bỏ các chuỗi rỗng sinh ra do phân tách
                    .ToArray();                          // chuyển kết quả thành mảng string[]
                    //--------------------------------------------------------------------------------------------------------------------------
                    // Question: Trước khi có dòng ".ToArray();" thì nó sẽ là cái gì?
                    // Trước khi chuyển thành mảng bằng ToArray(), kết quả của Regex.Split(...).Where(...) là:
                    //   • Một đối tượng IEnumerable<string> – tức là “chuỗi truy vấn” LINQ chưa được thực thi ngay.
                    //   • Đây gọi là deferred execution: các phép tách và lọc chỉ được thực hiện khi bạn thực sự duyệt (enumerate) qua nó.
                    //   • Trong thực thi deferred:
                    //       - Không có bộ nhớ nào được cấp phát để lưu tất cả các phần tử ngay lập tức.
                    //       - Mỗi phần tử sẽ được tính toán “theo luồng” khi cần, ví dụ trong ToArray() hoặc vòng foreach.
                    //   • Khi gọi ToArray(), LINQ sẽ thực thi toàn bộ pipeline (Split, sau đó Where) và “vật chất hóa” (materialize)
                    //     kết quả thành một mảng string[], lưu giữ mọi phần tử cùng một lúc.
                    //
                    // Ví dụ trực quan:
                    //   var query = Regex.Split(content, @"\W+").Where(...);
                    //   // Ở bước này, query chỉ định “cách làm” chứ chưa chạy Split hay Where.
                    //   // Nếu không gọi ToArray() hay lặp qua query, không có gì xảy ra.
                    //   string[] words = query.ToArray();
                    //   // Khi ToArray() được gọi, Split và Where được thực thi hoàn chỉnh,
                    //   // và mọi giá trị kết quả mới được thu thập vào mảng `words`.
                    //--------------------------------------------------------------------------------------------------------------------------
                int wordCount = words.Length;
                Console.WriteLine($"2. Number of words: {wordCount}");

                // Todo: 2. Đếm số ký tự
                // ---------------------------------------------
                // IMPORTANT: Lambda expression trong Count
                // ---------------------------------------------
                // Note: "content.Count(c => !char.IsWhiteSpace(c))"
                // Var:  'c'      : biến tham số đại diện cho từng ký tự (char) trong content.
                // Var:  '=>'     : toán tử lambda, ngăn cách tham số (bên trái) với biểu thức trả về (bên phải).
                //   "char.IsWhiteSpace(c)":
                //           trả true nếu c là ký tự trắng (space, tab, newline, v.v.)
                //   "!char.IsWhiteSpace(c)":
                //           đảo nghĩa → true khi c KHÔNG phải ký tự trắng.
                // "Count(...)" sẽ đếm mỗi ký tự thỏa điều kiện.
                // ---------------------------------------------
                int charCountWithSpaces = content.Length;
                int charCountWithoutSpaces = content.Count(c => !char.IsWhiteSpace(c));
                Console.WriteLine($"3.1 Characters (with spaces): {charCountWithSpaces}");
                Console.WriteLine($"3.2 Characters (without spaces): {charCountWithoutSpaces}");

                // Todo: 3. Đếm số câu
                // ------------------------------------------------------------------------------------------------
                // IMPORTANT: Sử dụng "Regex.Matches" để tìm và đếm mọi dãy dấu kết thúc câu (., !, ?) trong chuỗi `content`
                // ------------------------------------------------------------------------------------------------
                // Note: 1) "Regex.Matches(input, pattern)":
                //    • Tĩnh phương thức của 'System.Text.RegularExpressions.Regex'.
                //    • Quét toàn bộ chuỗi `input`, tìm từng vị trí con khớp `pattern`.
                //    • Trả về 'MatchCollection' chứa mỗi `Match` ứng với một lần khớp.
                //    • 'Match' = một đoạn con của input khớp pattern (ở đây là dấu kết thúc câu).
                //
                // Note: 2) Pattern (@"[\.!?]+"):
                //    • '@': verbatim literal, giữ nguyên mọi ký tự mà không phải escape \\.
                //    • [...]: character class, khớp 1 ký tự bất kỳ bên trong.
                //      - ' \. ' → 'dấu chấm'.
                //      - ' ! ' → 'dấu chấm than'.
                //      - ' ? ' → 'dấu hỏi'.
                //    • '+': quantifier, khớp 1 hoặc nhiều ký tự liền kề trong class.
                //    → Mỗi dãy ".", "?!", "!!!", "..." đều tính là 1 match.
                //
                // Question: 3) Tại sao dùng 'Matches' mà không dùng 'Split'?
                //    - 'Matches' chỉ trả số lần dấu kết thúc câu xuất hiện (đúng mục đích đếm câu).
                //    - 'Split' sẽ tách nội dung thành các phần (giữa) các dấu, 
                //      dẫn đến mảng kết quả có phần tử rỗng hoặc phải xử lý trừ đi 1,
                //      phức tạp hơn và dễ sai số khi nhiều dấu liên tiếp.
                //    - Với 'Matches', ta chỉ cần lấy `.Count` là ra ngay số câu.
                //
                // Important: Ví dụ minh họa:
                //   content = "Hi! Are you OK? Yes..."
                //   'Matches' → ["!", "?", "..."] → 'Count = 3'
                //   'Split'  → ["Hi", " Are you OK", " Yes", ""] → 'phải lọc/trừ → rườm rà'
                // ------------------------------------------------------
                int sentenceCount = Regex.Matches(content, @"[\.!?]+").Count;
                Console.WriteLine($"4. Number of sentences: {sentenceCount}");

                // Todo: 4. Top 5 từ     
                // -----------------------------------------------------------------------------
                // IMPORTANT: Tìm và hiển thị Top 5 từ xuất hiện nhiều nhất trong mảng `words`
                // -----------------------------------------------------------------------------
                // Note: Quá trình xử lý từng bước:
                // 
                // Note: 
                //   Bước 1: "GroupBy(w => w.ToLower())":
                //   • Gom các từ giống nhau thành từng nhóm riêng biệt.
                //   • 'w.ToLower()': chuyển từ về chữ thường để không phân biệt chữ hoa, chữ thường.
                //   • Ví dụ minh họa:
                //       words = ["Hello", "hello", "Hi", "HI", "test", "Test"]
                //       → nhóm 1 ("hello"): ["Hello", "hello"]
                //       → nhóm 2 ("hi"): ["Hi", "HI"]
                //       → nhóm 3 ("test"): ["test", "Test"]
                //
                // Note:
                //   Bước 2: "OrderByDescending(g => g.Count())":
                //   • Sau khi có các nhóm, sắp xếp theo số lần xuất hiện giảm dần.
                //   • 'g.Count()': đếm số lượng từ bên trong mỗi nhóm.
                //   • Ví dụ: Nếu "hello" xuất hiện 10 lần, "hi" 7 lần, thì nhóm "hello" được xếp trước.
                //
                // Note: 
                //   Bước 3: "Take(5)":
                //   • Chỉ lấy 5 nhóm đầu tiên (top 5 nhóm có số lần xuất hiện nhiều nhất).
                //
                // Note:
                //   Bước 4: "Select(g => new { Word = g.Key, Count = g.Count() })":
                //   • Tạo một đối tượng mới dễ nhìn, gồm hai thuộc tính:
                //       - 'Word': từ đại diện cho nhóm đó.
                //       - 'Count': số lần từ đó xuất hiện.
                //
                // Note:
                //   Bước 5: Dùng vòng lặp 'foreach' để hiển thị rõ ràng, dễ đọc ra màn hình.
                //   • Lặp qua từng phần tử trong 'topWords' (đã được lấy ra ở bước 4).
                //   • In mỗi từ cùng với số lần xuất hiện tương ứng.
                //
                // Note:
                // Ví dụ kết quả cuối cùng được hiển thị ra màn hình:
                //   5. Top 5 most common words:
                //      - "hello": 10 lần
                //      - "hi": 9 lần
                //      - "world": 7 lần
                //      - "csharp": 5 lần
                //      - "regex": 3 lần
                // ----------------------------------------------------------------------
                var topWords = words
                    .GroupBy(w => w.ToLower())                 // Gom các từ giống nhau (không phân biệt hoa/thường)
                    .OrderByDescending(g => g.Count())         // Sắp xếp nhóm theo số lượng giảm dần
                    .Take(5)                                   // Chọn ra 5 từ xuất hiện nhiều nhất
                    .Select(g => new
                    {                         // Tạo đối tượng chứa từ và số lần xuất hiện
                        Word = g.Key,
                        Count = g.Count()
                    });

                Console.WriteLine("5. Top 5 most common words:");
                foreach (var item in topWords)
                {
                    // ---------------------------------------------------------------------------------
                    // Question: Vì sao lại có dấu [\"] trong câu lệnh in? (Escape character trong C#)
                    // ---------------------------------------------------------------------------------
                    // Khi bạn viết dấu [\"] trong chuỗi, dấu \ (backslash) có ý nghĩa ("escape"):
                    //   • Nó báo cho trình biên dịch biết rằng dấu ( " ) theo sau không phải là dấu kết thúc chuỗi,
                    //     mà là (ký tự ngoặc kép thật sự) trong nội dung chuỗi cần hiển thị.
                    // Ví dụ:
                    //   "Console.WriteLine(" \"Hello\" "); sẽ in ra "Hello" (có dấu ngoặc kép).
                    //
                    // Trong trường hợp dưới đây, [ \"{item.Word}\" ] giúp hiển thị từ được bao quanh bởi dấu ngoặc kép.
                    // Ví dụ kết quả hiển thị sẽ là:
                    //   - "hello": 10 lần
                    Console.WriteLine($"   - \"{item.Word}\": {item.Count} lần");
                }


                // Todo: 5. Độ dài trung bình của từ
                // -----------------------------------------------------------------
                // IMPORTANT: Tính độ dài trung bình của các từ trong mảng `words`.
                // -----------------------------------------------------------------
                //
                // Note:
                // 1) "wordCount > 0 ? words.Average(w => w.Length : 0"
                //    • Kiểm tra xem có ít nhất một từ hay không (wordCount > 0).
                //    • Nếu có (true): thực hiện tính trung bình.
                //    • Nếu không (false): gán giá trị 0 để tránh lỗi tính trung bình của tập rỗng.
                //
                // Note:
                // 2) "words.Average(w => w.Length)"
                //    • Phương thức LINQ Average lấy trung bình cộng của giá trị do lambda cung cấp.
                //    • 'w => w.Length': với mỗi từ w, lấy {w.Length} (số ký tự của từ).
                //    • Kết quả: tổng độ dài các từ chia cho số từ.
                //
                // Note:
                // 3) ": 0"
                //    • Trả về 0 nếu không có từ nào, đảm bảo chương trình không bị lỗi chia cho 0.
                //
                // Note:
                // 4) "Console.WriteLine($"...{averageWordLength:F2}...")"
                //    • 'String interpolation với định dạng F2': làm tròn và hiển thị 2 chữ số thập phân.
                //    • Ví dụ: 5.3333 → "5.33"
                //
                // Note:
                // Ví dụ minh họa:
                //   words = ["Hello","CSharp","Regex"]
                //   Độ dài từng từ: 5, 6, 5 → Trung bình = (5+6+5)/3 = 5.33
                // ------------------------------------------------------
                double averageWordLength = wordCount > 0
                    ? words.Average(w => w.Length)
                    : 0;
                Console.WriteLine($"6. Average word length: {averageWordLength:F2} ký tự");

                // TODO: Additional analysis to be implemented
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}

// -----------------------------------------------------------------------------------------------------------------
// Question:
// Các phương thức LINQ đã sử dụng trong toàn bộ quá trình phân tích:
// 1) "Where(predicate)"           – Lọc các phần tử theo điều kiện (loại bỏ chuỗi rỗng).
//
// 2) "ToArray()"                  – Chuyển IEnumerable<T> thành mảng T[].
//
// 3) "Count(predicate)"           – Đếm phần tử thỏa mãn predicate (ví dụ ký tự không phải khoảng trắng).
//    • Lưu ý: "Regex.Matches(...).Count" là API của MatchCollection, không phải LINQ.
//
// 4) "GroupBy(keySelector)"       – Gom nhóm các phần tử theo khóa (ví dụ từ đã lowercase).
//
// 5) "OrderByDescending(keySelector)" – Sắp xếp giảm dần dựa trên khóa (ví dụ số phần tử mỗi nhóm).
//
// 6) "Take(n)"                    – Lấy n phần tử đầu tiên trong tập đã sắp xếp.
//
// 7) "Select(selector)"           – Chiết xuất hoặc biến đổi mỗi phần tử thành dạng khác (đối tượng ẩn danh).
//
// 8) "Average(selector)"          – Tính giá trị trung bình của tập giá trị do selector cung cấp.
//
// -----------------------------------------------------------------------------------------------------------------
