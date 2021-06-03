/*****************************************************************************
* LED flash/PWM clock generate module
* 2015/03/19 A.Suzuki
******************************************************************************/
module flashLED (
	input clk,
	output reg flashclk
);

initial 
begin
	flashclk=1'b1;
	oncount=16'b0000000000000000;
	phasecount=16'b0000000000000000;
end
 

reg [16:0] oncount;
reg [16:0] phasecount;

parameter ontime=512;			//LED ON width
parameter phasetime=1024;		//LED blink phase width

always @(posedge clk)
begin

	
	oncount <= oncount + 1'b1;
	phasecount <= phasecount + 1'b1;
	
	if (oncount <= ontime)begin
	flashclk=1'b1;
	end else begin
	flashclk=1'b0;
	end
	
	if (phasecount == phasetime)begin
	oncount <= 16'b0000000000000000;
	phasecount <= 16'b0000000000000000;	
	end

end

endmodule
