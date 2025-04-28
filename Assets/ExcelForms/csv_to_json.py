import pandas as pd
import json
import os
import ast

# 获取脚本所在目录
script_dir = os.path.dirname(os.path.abspath(__file__))

# 配置输入和输出文件名（可以根据需要修改）
input_csv = 'Items.csv'  # 替换为 'Items.csv' 或其他文件名
output_json = input_csv.replace('.csv', '.json')

# 构建输入和输出路径
csv_path = os.path.join(script_dir, input_csv)
json_path = os.path.join(script_dir, output_json)

# 解析字符串形式的 JSON 数组
def parse_json_string(s):
    if not isinstance(s, str):  # 如果不是字符串，直接返回原值
        return s
    try:
        # 检查是否形如 JSON 数组（以 [ 开头，] 结尾）
        if s.startswith('[') and s.endswith(']'):
            return ast.literal_eval(s)  # 安全解析为 Python 列表
        return s  # 非 JSON 数组字符串，保持不变
    except (ValueError, SyntaxError) as e:
        print(f"解析错误: {s} - {e}")
        return s  # 解析失败，返回原值

# 读取 CSV
try:
    df = pd.read_csv(csv_path, encoding='utf-8')
except Exception as e:
    print(f"读取 CSV 失败: {e}")
    exit(1)

# 自动处理所有列，解析字符串形式的 JSON 数组
for column in df.columns:
    df[column] = df[column].apply(parse_json_string)

# 转成 JSON
data = {input_csv.replace('.csv', ''): df.to_dict(orient='records')}

# 保存 JSON
try:
    with open(json_path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=4, ensure_ascii=False)
    print(f"转换成功！生成文件: {json_path}")
except Exception as e:
    print(f"保存 JSON 失败: {e}")
    exit(1)