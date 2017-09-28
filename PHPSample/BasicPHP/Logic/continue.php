<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Continue</title>
	</head>
	<body>

		<?php
			for ($count=0; $count <= 10; $count++) {
				if ($count % 2 == 0) {
          //contine makes the loop start again
          continue;
        }
				echo $count . ", ";
			}
		?>

		<?php
			$count = 0;
			while ($count <= 10) {
				if ($count == 5) {
          //do not forget that 'continue' ignore the rest part of loop
					$count++;
					continue;
				}
				echo $count . ", ";
				$count++;
			}
		?>

		<br />
		<?php // loop inside a loop with continue

			for ($i=0; $i<=5; $i++) {
				if ($i % 2 == 0) { continue(1); }
				for ($k=0; $k<=5; $k++) {
          //'continue(n)' = skip loop as much as 'n'
					if ($k == 3) { continue(2); }
			  	echo $i . "-" . $k . "<br />";
				}
			}

		?>


	</body>
</html>
