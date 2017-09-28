<!DOCTYPE html>

<html lang="en">
	<head>
		<title>Form Processing</title>
	</head>
	<body>

		<pre>
		<?php
			//print out POST
			print_r($_POST);
		?>
		</pre>
		<br />
		<?php
			// detect form submission (it use name to detect 'submit')
			if (isset($_POST['submit'])) {
				echo "form was submitted<br />";
				// set default values (defualt value is important)
				if (isset($_POST["username"])) {
					$username = $_POST["username"];
				} else {
					$username = "";
				}
				if (isset($_POST["password"])) {
					$password = $_POST["password"];
				} else {
					$password = "";
				}

				// better way to set value
				// set default values using ternary operator
				//   boolean_test ? value_if_true : value_if_false
				$username = isset($_POST['username']) ? $_POST['username'] : "";
				$password = isset($_POST['password']) ? $_POST['password'] : "";

			} else {
				$username = "";
				$password = "";
			}
		?>

		<?php
			echo "{$username}: {$password}";
		?>

	</body>
</html>
