// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#exampleModal1').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget) 
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})

$('#exampleModal2').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget) 
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})

$('#exampleModal3').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})

$('#exampleModal4').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	var id = button.data('id')
	var cont = button.data('cont')
	var btt = button.data('btt')

	var modal = $(this)
	modal.find('.modal-body .hola').val(id)
	//modal.find('.modal-body .modalcon').text(cont)
	//if (btt == "") {
	//	modal.find('.modal-footer .btn-primary').hide()

 //   }
	//modal.find('.modal-footer .btn-primary').text(btt)

})
$('#exampleModal5').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})

$('#exampleModal6').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})

$('#exampleModal7').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})
$('#exampleModal8').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})