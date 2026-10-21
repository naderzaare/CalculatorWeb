
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Main web page
app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Docker Calculator v2</title>

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

        input {
            padding: 12px;
            margin: 8px;
            width: 80%;
            box-sizing: border-box;
        }

        button {
            padding: 12px;
            margin: 8px;
            width: 80%;
            box-sizing: border-box;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .calculate-btn {
            background: #2563eb;
        }

        .clear-btn {
            background: #64748b;
        }

        button:hover {
            opacity: 0.9;
        }

        .version {
            color: #64748b;
            font-size: 12px;
            margin-top: 20px;
        }
    </style>
</head>

<body>
    <div class="card">
        <h2>Docker Calculator</h2>

        <input type="number" id="num1"
               placeholder="First number">

        <input type="number" id="num2"
               placeholder="Second number">

        <button class="calculate-btn"
                onclick="calculate()">
            Calculate
        </button>

        <button class="clear-btn"
                onclick="clearCalculator()">
            Clear
        </button>

        <h3 id="result">Result: -</h3>

        <div class="version">
            CalculatorWeb - Version 2.0
        </div>
    </div>

    <script>
        async function calculate() {
            const a = document.getElementById('num1').value;
            const b = document.getElementById('num2').value;

            if (a === '' || b === '') {
                document.getElementById('result').innerText =
                    'Please enter both numbers';
                return;
            }

            try {
                const response = await fetch(
                    '/api/add?a=' + encodeURIComponent(a) +
                    '&b=' + encodeURIComponent(b)
                );

                if (!response.ok) {
                    throw new Error('Invalid numbers');
                }

                const data = await response.json();

                document.getElementById('result').innerText =
                    'Result: ' + data.result;
            }
            catch (error) {
                document.getElementById('result').innerText =
                    'Calculation error';
            }
        }

        function clearCalculator() {
            document.getElementById('num1').value = '';
            document.getElementById('num2').value = '';
            document.getElementById('result').innerText =
                'Result: -';

            document.getElementById('num1').focus();
        }
    </script>
</body>
</html>
""", "text/html"));

// Addition API
app.MapGet("/api/add", (double a, double b) =>
{
    return Results.Ok(new
    {
        result = a + b
    });
});

app.Run();
