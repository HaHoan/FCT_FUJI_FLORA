-- WARNING: Do NOT edit the input and output ports in this file in a text
-- editor if you plan to continue editing the block that represents it in
-- the Block Editor! File corruption is VERY likely to occur.

-- Copyright (C) 2017  Intel Corporation. All rights reserved.
-- Your use of Intel Corporation's design tools, logic functions
-- and other software and tools, and its AMPP partner logic
-- functions, and any output files from any of the foregoing
-- (including device programming or simulation files), and any
-- associated documentation or information are expressly subject
-- to the terms and conditions of the Intel Program License
-- Subscription Agreement, the Intel Quartus Prime License Agreement,
-- the Intel MegaCore Function License Agreement, or other
-- applicable license agreement, including, without limitation,
-- that your use is for the sole purpose of programming logic
-- devices manufactured by Intel and sold by Intel or its
-- authorized distributors.  Please refer to the applicable
-- agreement for further details.


-- Generated by Quartus Prime Version 17.0 (Build Build 595 04/25/2017)
-- Created on Fri Jun 01 18:19:51 2018

LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;



--  Entity Declaration

ENTITY FILTER IS
-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
PORT
(
	RESET			: IN STD_LOGIC;
	FILTERCNT	: IN STD_LOGIC_VECTOR(3 downto 0);
	FILTERCLK	: IN STD_LOGIC;
	FREQ			: IN STD_LOGIC;
	FFREQ			: OUT STD_LOGIC
);
-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!

END FILTER;


--  Architecture Body

ARCHITECTURE FILTER_architecture OF FILTER IS
	
	signal COUNT		: std_logic_vector ( 3 downto 0 );
	

BEGIN

	process ( 
		RESET,
		FILTERCNT,
		FILTERCLK,
		FREQ
	)
	begin
		if ( RESET = '0' ) then
			FFREQ			<= '0';
			COUNT			<= ( others => '0' );
		
		elsif ( FILTERCNT = 0 ) then
			FFREQ			<= FREQ;
		elsif ( FILTERCLK'event and FILTERCLK = '1' ) then
			if ( FREQ = '0' ) then
				FFREQ			<= '0';
				COUNT			<= ( others => '0' );
			else
				if ( COUNT < FILTERCNT ) then
					COUNT			<= COUNT + '1';
					FFREQ			<= '0';
				else
					FFREQ			<= '1';
				end if;
			end if;
		else
			NULL;
		end if;
	end process;

	
END FILTER_architecture;