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
-- Created on Fri Apr 20 16:52:50 2018

LIBRARY ieee;
USE ieee.std_logic_1164.all;


--  Entity Declaration

ENTITY SELECTOR IS
-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
GENERIC(NN : INTEGER := 8);
PORT
(
IN0 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN1 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN2 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN3 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN4 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN5 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN6 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
IN7 : IN STD_LOGIC_VECTOR( NN-1 downto 0);
SEL : IN STD_LOGIC_VECTOR(2 downto 0);
INIT: IN STD_LOGIC;
INID:	IN	STD_LOGIC_VECTOR( NN-1 downto 0 );
OUTO : OUT STD_LOGIC_VECTOR( NN-1 downto 0)
);
-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!

END SELECTOR;


--  Architecture Body

ARCHITECTURE SELECTOR_architecture OF SELECTOR IS

	signal IN01 : std_logic_vector ( NN-1 downto 0 );
	signal IN23 : std_logic_vector ( NN-1 downto 0 );
	signal IN45 : std_logic_vector ( NN-1 downto 0 );
	signal IN67 : std_logic_vector ( NN-1 downto 0 );
	
	signal IN0123 : std_logic_vector ( NN-1 downto 0 );
	signal IN4567 : std_logic_vector ( NN-1 downto 0 );

	signal OUTDATA : std_logic_vector ( NN-1 downto 0 );
BEGIN
	
	
	
	
	-- １段目
	IN01 <= IN0 when SEL ( 0 ) = '0' else IN1;
	IN23 <= IN2 when SEL ( 0 ) = '0' else IN3;
	IN45 <= IN4 when SEL ( 0 ) = '0' else IN5;
	IN67 <= IN6 when SEL ( 0 ) = '0' else IN7;

	-- 2段目
	IN0123 <= IN01 when SEL( 1 ) = '0' else IN23;
	IN4567 <= IN45 when SEL ( 1 ) = '0' else IN67;
	
	-- 3段目
	OUTDATA <= IN0123 when SEL ( 2 ) = '0' else IN4567;
	
	-- 最終段
	OUTO <= OUTDATA when INIT = '0' else INID;

END SELECTOR_architecture;