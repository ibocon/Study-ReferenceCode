<?php require_once("../includes/session.php"); ?>
<!--create a database connection-->
<!--also test database connections-->
<?php require_once("../includes/db_connection.php"); ?>
<?php require_once("../includes/functions.php"); ?>

<!--'header' do the first part of HTML-->
<?php $layout_context = "admin"; ?>
<?php include("../includes/layouts/header.php"); ?>

<!--find out the page's value-->
<?php find_selected_page(); ?>

<div id="main">
  <div id="navigation">
    <!--'navigation' function take care the navigation-->
    <?php echo navigation($current_subject, $current_page); ?>
    <br/>
  </div>
  <div id="page">

    <?php echo message(); ?>
    <?php $errors = errors(); ?>
    <?php echo form_errors($errors); ?>

    <h2>Create Page</h2>
		<form  method="post" action="create_page.php?subject=<?php echo urlencode($current_subject["id"]); ?>">
		  <p>Menu name:
		    <input type="text" name="menu_name" value="" />
		  </p>
		  <p>Position:
		    <select name="position">
          <?php
            $page_set = find_pages_for_subject($current_subject["id"], false);
            $page_count = mysqli_num_rows($page_set);
            for($count=1; $count <= ($page_count + 1); $count++){
              echo "<option value=\"{$count}\"> {$count} </option>";
            }
          ?>
		    </select>
		  </p>
		  <p>Visible:
        <!--return boolean value by '0' or '1'-->
		    <input type="radio" name="visible" value="0" /> No
		    &nbsp;
		    <input type="radio" name="visible" value="1" /> Yes
		  </p>
      <p>Content: <br/>
        <textarea name="content" cols="40" rows="10"></textarea>
      </p>
		  <input type="submit" name="submit" value="Create Page" />
    </form>
		<br />
		<a href="manage_content.php">Cancel</a>
  </div>
</div>

<!--'footer' do the last part of HTML-->
<?php include("../includes/layouts/footer.php"); ?>
