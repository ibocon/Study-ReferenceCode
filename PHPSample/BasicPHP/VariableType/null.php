<!DOCTYPE html>
<html lang="en">
	<head>
		<title>NULL</title>
	</head>
	<body>

		<?php
			$var1 = null;
			$var2 = "";
		?>
		var1 null? <?php echo is_null($var1); //True ?><br />
		var2 null? <?php echo is_null($var2); //false ?><br />
		var3 null? <?php echo is_null($var3); //Notice: undefined -> null -> True ?><br />
		<br />
		var1 is set? <?php echo isset($var1); //False ?><br />
		var2 is set? <?php echo isset($var2); //True ?><br />
		var3 is set? <?php echo isset($var3); //False ?><br />
		<br />

		<?php // empty: "", null, 0, 0.0, "0", false, array() ?>

		<?php $var3 = "0"; ?>
        <!--null is also empty. It makes a lot of bug in PHP code-->
		var1 empty? <?php echo empty($var1); //True ?><br />
		var2 empty? <?php echo empty($var2); //True ?><br />
		var3 empty? <?php echo empty($var3); //True ?><br />

	</body>
</html>
