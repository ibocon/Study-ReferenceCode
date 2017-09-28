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
    <br />
    <a href="admin.php">&laquo; Main menu</a><br/>
    <!--'navigation' function take care the navigation-->
    <?php echo navigation($current_subject, $current_page); ?>
		+ <a href="new_subject.php">Add a subject </a>
  </div>
  <div id="page">

    <?php echo message(); ?>

		<?php if($current_subject) {?>
			<h2>Manage Subject</h2>
      Menu name: <?php echo htmlentities($current_subject["menu_name"]); ?><br />
      Position: <?php echo $current_subject["position"]; ?><br />
      Visible: <?php echo $current_subject["visible"] == 1? 'Yes' : 'No'; ?><br />

      <a href="edit_subject.php?subject=<?php echo urlencode($current_subject["id"]); ?>">Edit Subject</a>

      <hr/>
      <div style="margin-top: 2em; border-top: 1px solid #000000;">
        <h3>Pages in this subject: </h3>
        <?php
          //query the current subject's pages.
          $page_list= find_pages_for_subject($current_subject["id"],false);
          //make list of current subject's pages.
          echo "<ul class=\"pages\">";
          foreach($page_list as $one_page){
            echo "<li> <a href=\"manage_content.php?page=";
            echo urlencode($one_page["id"]);
            echo "\">";
            echo htmlentities($one_page["menu_name"]);
            echo "</a></li>";
          }
          echo "</ul>";
        ?>
        <br/>
        +<a href="new_page.php?subject=<?php echo urlencode($current_subject["id"]); ?>"> Add a new page to this subject</a>
      </div>

    <?php } elseif ($current_page) { ?>

      <h2>Manage Page</h2>
      Menu name: <?php echo htmlentities($current_page["menu_name"]); ?><br />
      Position: <?php echo $current_page["position"]; ?><br />
      Visible: <?php echo $current_page["visible"] == 1? 'Yes' : 'No'; ?><br />
      Content: <br />
      <div class="view-content">
        <?php echo htmlentities($current_page["content"]); ?>
      </div></br>
      <a href="edit_page.php?page=<?php echo urlencode($current_page["id"]); ?>">Edit Page</a>

    <?php } else { ?>
      Please select a subject or a page.
    <?php }?>

  </div>
</div>

<!--'footer' do the last part of HTML-->
<?php include("../includes/layouts/footer.php"); ?>
