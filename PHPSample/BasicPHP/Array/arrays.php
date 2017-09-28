<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Arrays</title>
	</head>
	<body>
		<?php
		//This is the simple way to create array
			$numbers = array(4,8,15,16,23,42);
			//using [] to reference array
			echo $numbers[0];
		?>
		<br />

		<?php $mixed = array(6, "fox", "dog", array("x", "y", "z")); ?>
		<?php echo $mixed[2]; ?><br />
		<!-- this is how to search nested array by using two []-->
		<?php echo $mixed[3][1]; ?><br />

		<!--change value in array-->
		<?php $mixed[2] = "cat"; ?>
		<?php $mixed[4] = "mouse"; ?>
		<?php $mixed[] = "horse"; ?>

		<pre><!--this helps to read array to debug code-->
		<?php echo print_r($mixed); ?>
		</pre>

		<?php
			//PHP 5.4 added the short array syntax.
			$array = [1,2,3];
		?>

	</body>
</html>
