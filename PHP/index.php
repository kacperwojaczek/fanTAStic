<?php
	include_once "config.php";
?>

<html>
<?php echo head(); ?>
<body>
	<div id="modal-control" <?php if($_COOKIE && $_COOKIE["modal-launch"] === "hidden") {?> class="hidden" <?php } ?>>
	<?php echo navbar(); ?>
	<main>
		<?php echo halloffame(); ?>
	</main>
	<?php echo footer(); ?>
	</div>
	<div id="modal-wrapper" <?php if($_COOKIE && $_COOKIE["modal-launch"] && $_COOKIE["modal-launch"] === "hidden") {?> class="hidden" <?php } ?>>
<?php if($_COOKIE["modal-launch"] !== "hidden") { ?>
		<?php echo splashModal(); ?>
<?php } ?>
	</div>
</body>
</html>