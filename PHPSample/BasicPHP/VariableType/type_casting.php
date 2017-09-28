<!DOCTYPE html>

<html lang="en">
	<head>
		<title>Type Juggling and Type Casting</title>
	</head>
	<body>

		Type Juggling<br />
		<?php $count = "2 cats";//use concatenation is better coding?>
		Type: <?php echo gettype($count); //string ?><br />

		<?php $count += 3; ?>
		Type: <?php echo gettype($count); //becomes integer ?><br />

		<?php $cats = "I have " . $count . " cats."; ?>
		concatenation: <?php echo gettype($cats); //string ?><br />
		<br />

		Type Casting<br />
		<?php settype($count, "integer");//count=string to integer ?>
		count: <?php echo gettype($count); ?><br />

		<?php $count2 = (string) $count; //does not change count's type, count2 becomes string ?>
		count: <?php echo gettype($count); //integer ?><br />
		count2: <?php echo gettype($count2); //string ?><br />
		<br />

		<?php $test1 = 3; ?>
		<?php $test2 = 3; ?>
		<?php settype($test1, "string"); //test1 becoms string type ?>
		<?php (string) $test2; //does not change test2's type permanently to stirng, it still integer. ?>
		test1: <?php echo gettype($test1); //string ?><br />
		test2: <?php echo gettype($test2); //integer ?><br />

	</body>
</html>
