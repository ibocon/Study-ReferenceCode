<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
   "http://www.w3.org/TR/html4/loose.dtd">

<html lang="en">
  <head>
    <title>First Page</title>
  </head>
  <body>

    <?php $link_name = "Second Page"; ?>
    <!--print out "Second Page" as link's name-->
    <a href="second_page.php"><?php echo $link_name;?></a>

    <?php $id = 5; ?>
    <?php $company = "Johnson & Johnson"; ?>
    <!--Send the variable to 'second_page.php', id & company-->
    <!--Super global variable-->
    <a href="second_page.php?id=<?php echo $id; ?>&company=<?php echo rawurlencode($company); ?>"><?php echo $link_name; ?></a>

  </body>
</html>
