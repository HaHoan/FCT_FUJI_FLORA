/*****************************************************************************
* anti chattering SW input module with invered output
* 2015/03/19 A.Suzuki
******************************************************************************/
module anti_chatter (
    input       swin,
    input       clk,
    output reg  invout	//output logic level is inverted (ON=1,OFF=0)
);

    reg [7:0] shiftreg;      //shift register for SW-input samples

    always @ (posedge clk)
    begin
        shiftreg[7:0] <= {shiftreg[6:0],swin}; //shift in input status string
        if (shiftreg[7:0] == 8'b00000000) begin
            invout <= 1'b1; //if string is all 0, turn output 1 (SW_on)
        end else if (shiftreg[7:0] == 8'b11111111) begin
            invout <= 1'b0; //if string is all 1, turn output 0 (SW_off)
        end else begin
            invout <= invout; //other, keep current status (initial value=0)
        end
    end

endmodule
