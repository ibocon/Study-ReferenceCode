/*
  HTTP has
  Date:
  Server:
  Content-Type:
  Content-Length:
  Connection:
  Location:
*/
//'header' is the first thing to care BEFORE HTML!!
<?php
  header("HTTP 1.1/ 404 Not Found");
  header("X-Powered-By: none of your business");
?>

<!DOCTYPE html>
<html lang="en">
  <head>
    <title>Headers</title>
  </head>
  <body>

    <?php
      // This won't work... unless you have output
      // buffering turned on.
      // header("HTTP 1.1/ 404 Not Found");
    ?>

    <pre>
      <?php
      //print header change
        print_r(headers_list());
      ?>
    </pre>
  </body>
</html>
