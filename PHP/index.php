<?php
	include_once "config.php";
?>

<html>
<?php head(); ?>
<body>
<?php if($_COOKIE["modal-launch"] !== "hidden") { ?>
	<div id="modal-control">
<?php } ?>
	<?php navbar(); ?>
	<main>
		<?php halloffame(); ?>
	</main>
	<?php footer(); ?>
<?php if($_COOKIE["modal-launch"] !== "hidden") { ?>
	</div>
	<div id="modal-wrapper">
		<div id="modal">
			<?php logo("", ""); ?>
			<?php login(); ?>
			<?php register(); ?>
			<div id="explore">
				<h2>or</h2>
				<div id="modal-close">Explore the Community</div>
			</div>
		</div>
	</div>
<?php } ?>
</body>
</html>