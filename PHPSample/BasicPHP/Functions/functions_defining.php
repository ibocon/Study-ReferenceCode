<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Functions: Defining</title>
	</head>
	<body>
		<?php
		  //how to define function!
			function say_hello() {
				echo "Hello World!<br />";
			}
		  //how to call function!
			say_hello();

      //how to toss value to function
			function say_hello_to($word) {
				echo "Hello {$word}!<br />";
			}

			say_hello_to("World");
			say_hello_to("Everyone");

			say_hello_loudly();
      //after defining function is working!
			function say_hello_loudly() {
				echo "HELLO WORLD!<br />";
			}

			// function say_hello_loudly() {
			// 	echo "We can't redefine a function.";
			// }

		?>
	</body>
</html>
