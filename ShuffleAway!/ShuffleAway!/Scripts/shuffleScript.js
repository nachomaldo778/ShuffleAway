$('button[type="submit"]').click(function () {
	console.log('asd');
	$(this).html('<span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>').addClass('disabled');
});