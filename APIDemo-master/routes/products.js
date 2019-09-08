var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/', function(req, res, next) {
	connection.query('SELECT * from products', function (error, results, fields) {
		if(error){
			res.send(JSON.stringify({"status": 500, "error": error, "response": null})); 
	  		//If there is error, we send the error in the error section with 500 status
	  	} else {
	  		res.send(JSON.stringify({"status": 200, "success": null, "response": results}));
  			//If there is no error, all is good and response is 200OK.
  		}
  	});
});

router.get('/:id', function(req, res, next) {
	let id = req.params.id;

	connection.query('SELECT * from products Where id=' + id , function (error, results, fields) {
		if(error){
			res.send(JSON.stringify({"status": 500, "error": error, "response": null})); 
	  		//If there is error, we send the error in the error section with 500 status
	  	} else {
	  		res.send(JSON.stringify({"status": 200, "success": null, "response": results}));
  			//If there is no error, all is good and response is 200OK.
  		}
  	});
});

router.post('/add' , function(req , res , next){
	var products = req.body;

	connection.query("Insert into `cafedb`.`products`(`pro_name`,`aciklama`,`fiyat`,`doviz`,`image`) Values('"+products.pro_name+"','"+products.aciklama+"',"+products.fiyat+",'"+products.doviz+"','" +products.image+"')", function(error, results, fields){
		if(error)
		{
			res.send({ error: false, data: results, message: 'New user has been created successfully.' });
		}
		else
		{
			res.send({ success: null, data: results, message: 'New product added.'});
		}	
	});
});

router.get('/delete/:id' , function(req , res , next) {
	let id = req.params.id;
	connection.query('Delete From `cafedb`.`products` Where id='+ id , function(error , results , fields) {
		if(error){
			res.send(JSON.stringify({"status":500 , "error": error , "response": "Ürün silinemedi."}));
		}
		else{
			res.send(JSON.stringify({"status":200 , "Success": null , "response": "Ürün başarıya silindi."}));
		}
	})
});

router.put('/:id' , function(req , res , next){
	let id = req.params.id;
	var products  = req.body;
	var sorgu = "Update `cafedb`.`products` Set pro_name='"+products.pro_name + "', aciklama='"+ products.aciklama + "', fiyat=" + products.fiyat + ", doviz='" + products.doviz + "', image='"+ products.image+"' Where id=" + id;
	connection.query(sorgu , function(error , results, fields0){
		if(error){
			res.send(JSON.stringify({"status":500 , "error": error , "response": "Ürün gümcellenemedi."}));
		}
		else{
			res.send(JSON.stringify({"status":200 , "success": null , "response": "Ürün başarıya güncellendi."}));
		}
	});
});

module.exports = router;
