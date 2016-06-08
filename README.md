# Reflection-vs.-Delegate
The purpose of this is to test the performance of different ways to map an entity and mainly comparing the performance of using Reflection and Delegates.

This test is mapping 20000 entities and each property gets the return value of some method. The results are listed:

--DirectInvoke---|---Reflection---|---Delegate---|--DelegateCached--

      164ms     |   1.338sec   |  1.807sec  |     388ms
