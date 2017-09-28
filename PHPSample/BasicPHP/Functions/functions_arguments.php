<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Functions: Arguments</title>
	</head>
	<body>
		<?php
      //how to toss value to function
		  function say_hello_to($word) {
		    echo "Hello {$word}!<br />";
		  }

			$name = "John Doe";
      say_hello_to($name);

		?>

		<?php
      //multivariable can works on it
			function better_hello($greeting, $target, $punct) {
				echo $greeting . " " . $target . $punct . "<br />";
			}

			better_hello("Hello", $name, "!");
			better_hello("Greetings", $name, "!!!");
      //same # of argument should be used!
      //null also converted into String but, nothing
			better_hello("Greetings", $name, null);

		?>

	</body>
</html>
