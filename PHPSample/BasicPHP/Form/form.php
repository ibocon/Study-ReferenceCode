<!DOCTYPE html>

<html lang="en">
	<head>
		<title>Form</title>
	</head>
	<body>
		<!--'action' attribute is used for handle form tag-->
		<form action="form_processing.php" method="post">
      <!--'name' attribute is used for index in php file-->
		  Username: <input type="text" name="username" value="" /><br />
		  Password: <input type="password" name="password" value="" /><br />
			<br />
		  <input type="submit" name="submit" value="Submit" />
		</form>

	</body>
</html>
