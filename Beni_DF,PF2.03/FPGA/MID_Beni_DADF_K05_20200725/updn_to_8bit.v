/*****************************************************************************
* Generate 8 bit Output from UP&DN momentary SW Input,
* byteout 14,15 is skipped (for WAIKIKI MCU conn TEST)
* 2015/03/19 A.Suzuki
* 2017/02/21 Modified for TIO input by A.Suzuki
******************************************************************************/
module updn_to_8bit (
    input clk,
    input up,
    input dn,
    output reg [7:0] byteout
);

initial
begin
	bytecount <= SWDef;									// set counter default
end

reg oldup;
reg olddn;
reg [7:0] bytecount;

parameter countmax=4;									// max SW channel (SWDef~255)
parameter countmin=0;									// min SW channel (0~SWDef)
parameter SWDef=0;										// default switch channel (countmin~countmax)
parameter AltEN=1;										//	1=Alternative / 0=limited Up Dn

always @(posedge clk)
begin
    if (dn == 1 && dn != olddn )begin				// detect rising edge of dn_SW
																// output (skipped)
		end
		
    else begin
		if (up == 1 && up != oldup )begin    		// detect rising edge of up_SW
			byteout <= bytecount;						//	output
//			if (bytecount == countmax)					// if current counter =countmax
			if (bytecount >= countmax)					// if current counter>=countmax
			// 三輪変更
			
				if (AltEN == 1)							// if alternative setttings
//					bytecount <= SWDef;					// turn default (for alternavive SW)
					bytecount <= 0;						// 三輪変更　turn 0 とする
				else											// if Upper limited settings
					bytecount <= countmax;				// keep max (for Non-alternative SW)
			else
				bytecount <= bytecount + 1'b1;		// add counter
			end
    end
    oldup = up; //save old value for edge detection
    olddn = dn; //save old value for edge detection
end

endmodule
