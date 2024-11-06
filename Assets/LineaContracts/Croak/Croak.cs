//------------------------------------------------------------------------------
// This code was generated by a tool.
//
//   Tool : MetaMask Unity SDK ABI Code Generator
//   Input filename:  Croak.sol
//   Output filename: Croak.cs
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// <auto-generated />
//------------------------------------------------------------------------------

using System;
using System.Numerics;
using System.Threading.Tasks;
using evm.net;
using evm.net.Models;

namespace Croak
{
	#if UNITY_EDITOR || !ENABLE_MONO
	[BackedType(typeof(CroakBacking))]
	#endif
	public interface Croak : IContract
	{
		[EvmMethodInfo(Name = "checkPot", View = false)]
		Task<Transaction> CheckPot();
		
		[EvmMethodInfo(Name = "prizeDistributer", View = false)]
		Task<Transaction> PrizeDistributer();
		
		[EvmMethodInfo(Name = "setScore", View = false)]
		Task<Transaction> SetScore(BigInteger score, CallOptions options = default);
		
		[EvmMethodInfo(Name = "transferOwnership", View = false)]
		Task<Transaction> TransferOwnership(EvmAddress newOwner, CallOptions options = default);
		
		[EvmConstructorMethod]
		Task<Croak> DeployNew();
		
		[EvmMethodInfo(Name = "getHighfirstdadd", View = true)]
		Task<EvmAddress> GetHighfirstdadd();
		
		[EvmMethodInfo(Name = "getHighFirstScore", View = true)]
		Task<BigInteger> GetHighFirstScore();
		
		[EvmMethodInfo(Name = "getHighseconddadd", View = true)]
		Task<EvmAddress> GetHighseconddadd();
		
		[EvmMethodInfo(Name = "getHighsecondScore", View = true)]
		Task<BigInteger> GetHighsecondScore();
		
		[EvmMethodInfo(Name = "getHightest", View = true, Returns = new[] {"uint256","uint256","uint256","address","address","address"})]
		[return: EvmParameterInfo(Type = "tuple")]
		Task<Tuple<BigInteger, BigInteger, BigInteger, EvmAddress, EvmAddress, EvmAddress>> GetHightest();
		
		[EvmMethodInfo(Name = "getHighthirdadd", View = true)]
		Task<EvmAddress> GetHighthirdadd();
		
		[EvmMethodInfo(Name = "getHighthirdScore", View = true)]
		Task<BigInteger> GetHighthirdScore();
		
		[EvmMethodInfo(Name = "getPlayerScore", View = true)]
		Task<BigInteger> GetPlayerScore(EvmAddress _playerAddress, CallOptions options = default);
		
		[EvmMethodInfo(Name = "getPotSetTime", View = true)]
		Task<BigInteger> GetPotSetTime();
		
		[EvmMethodInfo(Name = "getTotalStokenBalance", View = true)]
		Task<BigInteger> GetTotalStokenBalance();
		
	}
}