$(document).ready(function(){
	var itemArr = [];
	$('button').on('click',function() {
	$('.errCode').remove();	
	var name = $('#iName').val().trim();
	var amount = parseInt($('#iAmount').val());
	var price = parseFloat($('#iPrice').val());
	var errCode="";
	
	if (name.length < 1 || name.length >12){
	  errCode= $('<p class="errCode">Invalid name. Enter a name with less than 13 characters</p>');
	  errCode.insertAfter(this);
	}
	else if (isNaN(amount)){
	  errCode= $('<p class="errCode">Invalid amount. please enter an amount</p>');
	  errCode.insertAfter(this);
	}
	else if (amount <1 || amount>100){
	  errCode= $('<p class="errCode">Invalid amount. please enter a whole number between 1-100</p>');
	  errCode.insertAfter(this);
	}
	else if (isNaN(price)){
	  errCode= $('<p class="errCode">Invalid price. please enter a price</p>');
	  errCode.insertAfter(this);
	}
	else if ( price <.01 || price > 1000.00){
	  errCode= $('<p class="errCode">Invalid price. please enter a price between .01-1000.00</p>');
	  errCode.insertAfter(this);
	}
	else if ( itemArr.length > 30){
	errCode= $('<p class="errCode">Too many items, a max of 30 items can be tracked at one time.</p>');
	errCode.insertAfter(this);
	}
	else{
		var total = amount * price;
		total = parseFloat(total.toFixed(2));
		var item = {iName : name, iAmount : amount, iPrice: price, iTotal : total};
		itemArr.push(item);
		var iRow = $('<tr><td>'+item.iName+'</td><td>'+item.iAmount+'</td><td>$'+item.iPrice+'</td><td>$'+item.iTotal+'</td></tr>');
		var cTotal = 0;
		for(var i = 0; i < itemArr.length; i+=1) {
			cTotal += itemArr[i].iTotal;
		}		
		iRow.insertBefore('.lRow');
		cTotal = parseFloat(cTotal.toFixed(2));
		$('#total').text('$'+cTotal);		
	}
	});
});