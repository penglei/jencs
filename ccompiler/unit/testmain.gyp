{
	'includes': ['../build/common.gypi'],
	'targets': [
		{
			'type': 'executable',
			'dependencies': [
				'../src/neolib.gyp:neo', #必须要这个依赖，因为顶层应用(main.c, test.c)也直接用到了HDF对象
				'../lib/cs2clib.gyp:cs2c',
			],
			'target_name': 'testCsCompile',
			'sources': [
				'test_main.c',
				'test.c',#应该用生成的一个gyp文件来支持，这样最简洁
			],
		}
	],
}
