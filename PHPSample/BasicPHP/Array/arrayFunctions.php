<!DOCTYPE html>
<html lang="en">
<head>
  <title>Array's functions</title>
</head>
<body>
  <?php $num = array(2,4,23,17,22,15); ?>

  <!--Count how many values in there-->
  Count:  <?php echo count($num); ?><br/>
  <!--Find out the max value inside array-->
  Max value:  <?php echo max($num); ?><br/>
  <!--Find out the min value insdie array-->
  Min value:  <?php echo min($num); ?><br/>
  <br/>
  <pre>
    <!-- Sort the array, sorted array will be saved into origin-->
    Sort: <?php sort($num); print_r($num) ?><br/>
    <!-- Sort the array reversly-->
    Reverse Sort: <?php rsort($num); print_r($num); ?><br/>
  </pre>
  <br/>
  <!--It create the string list of array which is seperated by specified character-->
  Implode:  <?php echo $num_string = implode(" * ", $num); ?><br/>
  <!--It create the array from the string seperate the element by the specified character-->
  Explode:  <?php print_r(explode(" * ", $num_string)); ?><br/>
  <br/>
  <!--find out value in the array-->
  <?php $value=10 ?>
  FindValue: <?php echo in_array($value,$num); //returns T/F = 1/null ?>
</body>
</html>
