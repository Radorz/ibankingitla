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
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

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