// A simple calculator app that takes commands from the command line
using System;

// NOTE: CommandLineCalculator – ứng dụng máy tính dòng lệnh đơn giản
// NOTE: Nhận lệnh phép tính từ người dùng (add, subtract, multiply, divide)
// NOTE: Thực hiện tính toán và in kết quả
// NOTE: Gõ "exit" để thoát chương trình
namespace CommandLineCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to .NET Core Calculator!");
            Console.WriteLine("Available operations: add, subtract, multiply, divide");
            Console.WriteLine("Example usage: add 5 3");
            Console.WriteLine("Type 'exit' to quit");

            // IMPORTANT: Vòng lặp chính: nhận lệnh từ người dùng, xác thực và chuyển cho bộ xử lý.
            bool running = true;
            while (running)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                // NOTE: Nếu người dùng chỉ nhấn Enter hoặc nhập toàn khoảng trắng -> bỏ qua
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                // NOTE: Phân tích lệnh nhập
                // 1. Tách lệnh thành mảng [operation, number1, number2]
                // 2. Thao tác "Trim().Split(' ')"
                // 2.1. "Trim()": loại bỏ khoảng trắng đầu/cuối
                // 2.2. "Split(' ')": tách chuỗi ra mảng theo dấu cách
                string[] parts = input.Trim().Split(' ');

                // NOTE: Chuyển toàn bộ ký tự chữ hoa A–Z thành chữ thường a–z theo quy tắc culture hiện tại
                // NOTE: Trả về một chuỗi mới (immutable), không thay đổi chuỗi gốc
                // 1. Ví dụ:
                // 1.1. string s = "Hello WORLD!";
                // 1.2. "string lower = s.ToLower();" // "lower" sẽ là "hello world!"
                if (parts[0].ToLower() == "exit")
                {
                    running = false;
                    continue;
                }

                // WARNING: Nếu không đủ 3 phần (operation + 2 số) -> yêu cầu đúng định dạng
                if (parts.Length != 3)
                {
                    Console.WriteLine("Invalid input format. Please use: operation number1 number2");
                    continue;
                }

                // TODO: Implement parsing numbers and performing calculations
                // This is where you will add your code

                // Example implementation for addition:
                if (parts[0].ToLower() == "add")
                {
                    if (
                        // VAR: "double.TryParse(parts[1], out double num1)" — chuyển parts[1] thành số thực
                        // IMPORTANT: Nếu (TryParse) thành công, "num1" chứa giá trị parsed
                        // WARNING: Nếu (TryParse) thất bại, "num1 = 0" và trả về false
                        double.TryParse(parts[1], out double num1)
                        // QUESTION: && chỉ chạy tiếp khi biểu thức trước trả về true (short-circuit)
                        &&
                        // VAR: "double.TryParse(parts[2], out double num2)" — chuyển parts[2] thành số thực
                        // IMPORTANT: Nếu (TryParse) thành công, "num2" chứa giá trị parsed
                        // WARNING: Nếu (TryParse) thất bại, "num2 = 0" và trả về false
                        double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 + num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }

                // TODO: Implement subtract, multiply, divide operations

                // TODO: Xử lý lệnh "subtract": parse 2 số và in {num1 - num2} (hoặc báo lỗi nếu không phải số)
                if (parts[0].ToLower() == "subtract")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 - num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }

                // TODO: Xử lý lệnh "multiply": parse 2 số và in {num1 * num2} (hoặc báo lỗi nếu không phải số)
                if (parts[0].ToLower() == "multiply")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 * num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }
                
                // TODO: Xử lý lệnh "divide": parse 2 số và in {num1 / num2} (hoặc báo lỗi nếu không phải số)
                if (parts[0].ToLower() == "divide")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 / num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }
            }
        }
    }
}


