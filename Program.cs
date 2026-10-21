
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>My Docker Calculator</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f1f5f9;
            display: flex;
            justify-content: center;
            padding-top: 80px;
        }
        .card {
            background: white;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 4px 20px #0002;
            text-align: center;
            width: 350px;
        }
        input, button {
            padding: 12px;
            margin: 8px;
            width: 80%;
            box-sizing: border-box;
        }
        button {
            background: #2563eb;
            color: white;
            border: none;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <div class="card">
        <h2>Docker Calculator</h2>
        <input type="number" id="num1" placeholder="First number">
        <input type="number" id="num2" placeholder="Second number">
        <button onclick="calculate()">Calculate</button>
        <h3 id="result">Result: -</h3>
    </div>
    <script>
        async function calculate() {
            const a = document.getElementById('num1').value;
            const b = document.getElementById('num2').value;
            const response = await fetch(
                '/api/add?a=' + encodeURIComponent(a) +
                '&b=' + encodeURIComponent(b)
            );
            if (!response.ok) {
                document.getElementById('result').innerText = 'Invalid numbers';
                return;
            }
            const data = await response.json();
            document.getElementById('result').innerText =
                'Result: ' + data.result;
        }
    </script>
</body>
</html>
""", "text/html"));

app.MapGet("/api/add", (double a, double b) =>
    Results.Ok(new { result = a + b }));

app.Run();
