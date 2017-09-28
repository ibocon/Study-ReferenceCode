<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Booleans</title>
	</head>
	<body>

		<?php
		//assigning boolean value to variable
			$result1 = true;
			$result2 = false;
		?>
		Result1: <?php echo $result1; //result '1' ?><br />
		Result2: <?php echo $result2; //result 'null' ?><br />

		<!--check variable is boolean or not -->
		result2 is boolean? <?php echo is_bool($result2); ?>
		<br />

		<?php
		//boolean value is used for condition statment a lot
			$number = 3.14;
			if( is_float($number) //boolean value ) {
				echo "It is a float.";
			}

		?>
	</body>
</html>
