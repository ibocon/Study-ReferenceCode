<?php
	//just sample function that has
	//"hello"
	//"redirect_to"
	require_once("included_functions.php");
	//include basic validation fuctions
	//"validate_max_lengths"
	//"has_presence"
	require_once("validation_functions.php");

	$errors = array();
	$message = "";
	//check form is summited or not
	if (isset($_POST['submit'])) {
		// form was submitted

		//discard empty space
		$username = trim($_POST["username"]);
		$password = trim($_POST["password"]);

		// Validations
		$fields_required = array("username", "password");
		//check each fields_required's value
		foreach($fields_required as $field) {
			$value = trim($_POST[$field]);
			if (!has_presence($value)) {
				//add error message into 'errors' array
				//'ucfirst' make the first char to be an upper case
				$errors[$field] = ucfirst($field) . " can't be blank";
			}
		}

		//check length of each fields
		$fields_with_max_lengths = array("username" => 30, "password" => 8);
		validate_max_lengths($fields_with_max_lengths);

		if (empty($errors)) {
			// try to login
			if ($username == "kevin" && $password == "secret") {
				// successful login
				redirect_to("basic.html");
			} else {
				$message = "Username/password do not match.";
			}
		}

	} else {
		$username = "";
		$message = "Please log in.";
	}
?>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Form</title>
	</head>
	<body>

		<?php echo $message; ?><br />
		<?php echo form_errors($errors); ?>

		<form action="form_with_validation.php" method="post">
		  Username: <input type="text" name="username" value="<?php echo htmlspecialchars($username); ?>" /><br />
		  Password: <input type="password" name="password" value="" /><br />
			<br />
		  <input type="submit" name="submit" value="Submit" />
		</form>

	</body>
</html>
