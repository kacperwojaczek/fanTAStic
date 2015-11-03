<?php
	include "config.php";
?>
<html>
<?php head($site, "główna"); ?>
<body>
	<!--?php //include "structure/nav.php" ?-->
	<main>
	<!--?php //include "structure/halloffame.php" ?-->
	</main>
	<aside>
		<?php login(); ?>
		<?php register(); ?>
	</aside>
	<!--?php //include "structure/footer.php" ?-->
</body>
</html>