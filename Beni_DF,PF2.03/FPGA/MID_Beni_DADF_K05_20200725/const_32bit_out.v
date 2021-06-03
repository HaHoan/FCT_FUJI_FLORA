/*****************************************************************************
* 32bit_Constant_Output
* 2017/02/23 A.Suzuki
******************************************************************************/
module const_32bit_out (
	output wire [31:0] Constant_out
);

parameter Param_32bit=32'h55555555;
assign Constant_out = Param_32bit;				//	output

endmodule
